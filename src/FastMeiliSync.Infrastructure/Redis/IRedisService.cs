namespace FastMeiliSync.Infrastructure.Redis;

public interface IRedisService
{
    Task PublishMessageAsync(
        string channel,
        string message,
        CancellationToken cancellationToken = default
    );
    Task ConsumeMessageAsync(string channel, CancellationToken cancellationToken = default);
}
