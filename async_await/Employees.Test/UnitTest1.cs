using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Employees.Test;

public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public EmployeesControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Test_Api_V1_Salary_Type_ReturnTrue()
    {
        var response = await _client.GetAsync("/api/v1/employees/10");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using(var document = JsonDocument.Parse(json))
        {
            var salary = document.RootElement.GetProperty("employeeSalary");

            Assert.Equal(JsonValueKind.Number, salary.ValueKind);
        }
    }

    [Fact]
    public async Task Measure_Sequential_Vs_Parallel_Execution_Time()
    {
        var ids = Enumerable.Range(1, 20).ToList();
        const int rounds = 10;

        // warm up (JIT, routing, EF model) so it doesn't skew the first measurement
        await _client.GetAsync("/api/v1/employees/1");

        var stopwatch = new Stopwatch();
        double sequentialTotal = 0, parallelTotal = 0;

        for (int round = 1; round <= rounds; round++)
        {
            // sequential: await each request before starting the next one
            stopwatch.Restart();
            foreach (var id in ids)
            {
                var response = await _client.GetAsync($"/api/v1/employees/{id}");
                response.EnsureSuccessStatusCode();
            }
            stopwatch.Stop();
            var sequential = stopwatch.Elapsed.TotalMilliseconds;

            // parallel: start all requests, then await them together
            stopwatch.Restart();
            var responses = await Task.WhenAll(ids.Select(id => _client.GetAsync($"/api/v1/employees/{id}")));
            stopwatch.Stop();
            var parallel = stopwatch.Elapsed.TotalMilliseconds;

            Assert.All(responses, r => r.EnsureSuccessStatusCode());

            sequentialTotal += sequential;
            parallelTotal += parallel;

            _output.WriteLine($"Round {round,2}: sequential = {sequential,8:F2} ms | parallel = {parallel,8:F2} ms");
        }

        _output.WriteLine($"Average : sequential = {sequentialTotal / rounds,8:F2} ms | parallel = {parallelTotal / rounds,8:F2} ms");
        _output.WriteLine($"Speedup : x{sequentialTotal / parallelTotal:F2}");
    }
}
