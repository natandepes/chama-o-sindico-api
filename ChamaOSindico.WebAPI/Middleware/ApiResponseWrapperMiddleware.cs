using System.Text.Json;

namespace ChamaOSindico.WebAPI.Middleware
{
    public class ApiResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Keep original body stream
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            if (context.Response.HasStarted)
            {
                return; // We can't change the response if headers were already sent
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);

            context.Response.Body = originalBodyStream;

            var statusCode = context.Response.StatusCode;

            // Skip if response is already an ApiResponse
            if (responseBody.TrimStart().StartsWith("{\"success\":"))
            {
                await context.Response.WriteAsync(responseBody);
                return;
            }

            var responseWrapper = new
            {
                success = statusCode >= 200 && statusCode < 300,
                message = statusCode >= 400 ? GetDefaultMessageForStatusCode(statusCode) : null,
                data = statusCode >= 200 && statusCode < 300 ? JsonSerializer.Deserialize<object>(responseBody) : null,
                statusCode = statusCode
            };

            context.Response.ContentType = "application/json";
            var wrappedJson = JsonSerializer.Serialize(responseWrapper);
            await context.Response.WriteAsync(wrappedJson);
        }

        private string GetDefaultMessageForStatusCode(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unexpected error"
        };
    }
}
