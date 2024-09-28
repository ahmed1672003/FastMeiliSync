using FastMeiliSync.Domain.Abstraction;

namespace FastMeiliSync.API.Middlewares;

public sealed class TokenGuard : IMiddleware
{
    readonly ILogger<TokenGuard> _logger;

    public TokenGuard(ILogger<TokenGuard> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var scope = context.RequestServices.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IMeiliSyncUnitOfWork>();
            if (!await unitOfWork.Tokens.AnyAsync(x => x.Content == token.ToString()))
            {
                var resposne = new Response
                {
                    Message = "Login again.",
                    StatusCode = 333,
                    Success = false
                };
                context.Response.StatusCode = 333;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(resposne));
                return;
            }
        }

        await next(context);
    }
}
