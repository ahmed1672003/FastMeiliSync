namespace FastMeiliSync.API.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var resposne = new Response
            {
                Message = "Oops!! something error",
                StatusCode = statusCode,
                Success = default(bool)
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(resposne));
        }
    }
}
