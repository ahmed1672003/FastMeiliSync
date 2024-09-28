using FluentValidation;

namespace FastMeiliSync.API.Middlewares;

public sealed class GlobalExceptionHandler : IMiddleware
{
    readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var requestStatus = GetRequestStatus(ex);
            var resposne = new Response
            {
                Message = requestStatus.Message,
                StatusCode = requestStatus.StatusCode,
                Success = false
            };
            context.Response.StatusCode = requestStatus.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(resposne));
        }
    }

    public (int StatusCode, string Message) GetRequestStatus(Exception ex)
    {
        switch (ex)
        {
            case ValidationException:
                _logger.LogInformation($"Un handeld exception accured: {ex.Message}");
                return ((int)HttpStatusCode.BadRequest, ex.Message);
            default:
                _logger.LogError($"Un handeld exception accured: {ex.Message}");
                return ((int)HttpStatusCode.InternalServerError, "Oops!! something error");
        }
    }
}
