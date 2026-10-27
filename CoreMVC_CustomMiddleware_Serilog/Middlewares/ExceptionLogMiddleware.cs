using System.Text.Json;

namespace CoreMVC_CustomMiddleware_Serilog.Middlewares
{
    public class ExceptionLogMiddleware
    {
        RequestDelegate _next;
        ILogger<ExceptionLogMiddleware> _logger;
        public ExceptionLogMiddleware(RequestDelegate next,ILogger<ExceptionLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try{
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Critical Error:{context.Request.Path}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var responsePayLoad = new
            {
                statusCode = context.Response.StatusCode,
                message = "An internal server error occured.",
                errorType = ex.GetType().Name
            };
            var json = JsonSerializer.Serialize(responsePayLoad);
            await context.Response.WriteAsync(json);

        }
    }
}
