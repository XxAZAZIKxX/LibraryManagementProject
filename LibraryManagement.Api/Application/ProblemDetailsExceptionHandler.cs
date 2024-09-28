using LibraryManagement.Api.Core.Exceptions;
using LibraryManagement.Api.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Application;

internal class ProblemDetailsExceptionHandler(IProblemDetailsService detailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Extensions =
            {
                { "traceId", httpContext.TraceIdentifier },
                { "exception", new { details = exception.ToString() } },
                { "headers", httpContext.Request.Headers }
            }
        };
        if (httpContext.Request.Path is { HasValue: true } path) problemDetails.Extensions.Add("path", path.Value);

        if (httpContext.GetEndpoint() is { DisplayName: { } displayName })
            problemDetails.Extensions.Add("endpoint", displayName);

        if (exception is ControlledScenarioException scenarioException)
        {
            httpContext.Response.StatusCode = scenarioException.StatusCode;
            problemDetails.Extensions.Add("contextDetails", scenarioException.AdditionalInformation);
        }

        await detailsService.WriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext, Exception = exception, ProblemDetails = problemDetails
        });
        return true;
    }
}