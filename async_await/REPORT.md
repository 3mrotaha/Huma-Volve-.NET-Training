# Execution Time Report: Sequential vs Parallel

## Measurement Method
- Test: `Measure_Sequential_Vs_Parallel_Execution_Time` in `Employees.Test/UnitTest1.cs`.
- Tool: `System.Diagnostics.Stopwatch`.
- Operation: 20 `GET /api/v1/employees/{id}` requests (ids 1 to 20).
  - **Sequential:** each request is `await`ed before the next one starts (`foreach`).
  - **Parallel:** all requests are started at once, then `await Task.WhenAll(...)`.
- 10 rounds, after a warm-up request so JIT compilation and EF model building don't affect the first measurement.
- Run: `dotnet test --logger "console;verbosity=detailed"`

## Results

| Round | Sequential (ms) | Parallel (ms) |
|---|---|---|
| 1 | 24.89 | 2.86 |
| 2 | 8.73 | 1.77 |
| 3 | 14.99 | 2.63 |
| 4 | 8.20 | 2.22 |
| 5 | 8.15 | 1.61 |
| 6 | 7.98 | 1.44 |
| 7 | 7.83 | 1.60 |
| 8 | 12.64 | 1.77 |
| 9 | 7.90 | 1.35 |
| 10 | 8.12 | 1.31 |
| **Average** | **10.94** | **1.86** |

**Speedup ≈ 5.9x**

## Did parallelism actually improve performance?
**Yes.** The total time for the 20 requests dropped from ~11ms to ~2ms on average.

Reason: in the sequential approach, the total time is the sum of all request times, because each request waits for the previous one to finish.
In the parallel approach, the requests run at the same time, so the total time approaches the time of the slowest single request instead of the sum.

## Important Notes
- The parallelism here is between **independent requests**, and each request has its own scope and its own `DbContext`, so it is safe.
  It is **not** valid to run parallel operations on the same `DbContext` (e.g. `Task.WhenAll` inside the service),
  because `DbContext` is not thread-safe and EF Core will throw an exception.
- The measurement was done on an `InMemory` database and `TestServer` (no real network), so the numbers are very small and the first round fluctuates.
  With a real database and network latency, the absolute difference would be larger, but it would still be limited by the connection pool size and the number of cores.
- `async/await` alone (without `Task.WhenAll`) **does not speed up a single request**; its benefit is that it frees the thread while waiting,
  so the server can handle more requests. The speedup shown in the table comes from parallelism.
