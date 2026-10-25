using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Exceptions;
using Serilog;

namespace Products.Api.Exceptions;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem = exception switch
        {   
            FluentValidation.ValidationException ex => 
                    new ValidationProblemDetails(
                        ex.Errors.GroupBy(g => g.PropertyName)
                                    .ToDictionary(g => g.Key, 
                                            g => g.Select(e => e.ErrorMessage).ToArray()
                                    )
                    )
            {
                Title= "Validation Failed!",
                Status = StatusCodes.Status400BadRequest
            },
            NotFoundException ex => new ProblemDetails
            {
              Title = "Resource not found!",
              Detail = ex.Message
            },
            _ => new ProblemDetails
            {
                Title = "Server Error!",
                Detail = "An unhandled exception",
                Status = StatusCodes.Status500InternalServerError
            }
        };

        logger.LogTrace("request failed with status code {code}, {Title}, {Detail}", problem.Status!.Value, problem.Title, problem.Detail);
        httpContext.Response.StatusCode = problem.Status!.Value;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem
        });

        return true;
    }
}
