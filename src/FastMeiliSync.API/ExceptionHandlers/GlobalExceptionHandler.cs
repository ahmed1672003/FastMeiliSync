using FluentValidation;

namespace FastMeiliSync.API.ExceptionHandlers;

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
            _logger.LogError($"Un handeld exception accured: {ex.Message}");
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
                return ((int)HttpStatusCode.BadRequest, ex.Message);
            default:
                return ((int)HttpStatusCode.InternalServerError, "Oops!! something error");
        }
    }
}
