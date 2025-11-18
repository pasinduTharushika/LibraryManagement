using System.Net;
using System.Text.Json;

namespace LibraryManagement.Middlewares
{
    public class GlobalErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorLoggingMiddleware> _logger;

        public GlobalErrorLoggingMiddleware(RequestDelegate next, ILogger<GlobalErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // continue request pipeline
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "❌ An unhandled exception occurred for request {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                // Return standardized error response
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // default 500

            // Optional: handle specific exception types differently
            if (exception is ArgumentNullException) statusCode = HttpStatusCode.BadRequest;
            else if (exception is KeyNotFoundException) statusCode = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedAccessException) statusCode = HttpStatusCode.Unauthorized;

            var response = new
            {
                success = false,
                message = exception.Message, // hide details in production
                statusCode = (int)statusCode
            };

            var payload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
