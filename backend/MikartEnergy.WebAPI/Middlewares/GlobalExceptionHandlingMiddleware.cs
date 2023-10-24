using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MikartEnergy.WebAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;

        private string _bodyContent = string.Empty;

        public GlobalExceptionHandlingMiddleware(RequestDelegate requestDelegate, 
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _bodyContent = await ReadRequestBody(context.Request);
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType().ToString();
                var dateTime = DateTime.Now;

                _logger.LogError("[{@DateTime}] - EXCEPTION Handling Middleware - {@ExceptionType} - {@Exception} - {@RequestPath} - {@RequestHeaders} - {@RequestBody}", dateTime, exceptionType, exception, context.Request.Path.ToString(), context.Request.Headers, _bodyContent);

                // Creating respons with specific information about error.
                var problem = new ProblemDetails()
                {
                    // Alternatively, error information can be added here.
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "As internal server error has occurred"
                };

                string problemAsJson = JsonSerializer.Serialize(problem);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(problemAsJson);
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).
            request.EnableBuffering();
            request.Body.Position = 0;
            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            // Reset the request's body stream position for next middleware in the pipeline.
            request.Body.Position = 0;
            requestBody = requestBody ?? "";
            return requestBody;
        }

    }
}
