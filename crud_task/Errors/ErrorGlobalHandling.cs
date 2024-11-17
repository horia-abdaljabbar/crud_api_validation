using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace crud_task.Errors
{
    public class ErrorGlobalHandling : IExceptionHandler
    {
        private readonly ILogger<ErrorGlobalHandling> logger;

        public ErrorGlobalHandling(ILogger<ErrorGlobalHandling> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = exception.Message,

            };
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;

        }
    }
}
