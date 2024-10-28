using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace FastMeiliSync.Infrastructure.Redis;

public sealed class RedisService : IRedisService
{
    readonly ILogger _logger;
    readonly StackExchange.Redis.IDatabase _database;
    readonly IConnectionMultiplexer _redis;

    public RedisService(IConnectionMultiplexer redis, ILogger<RedisService> logger)
    {
        _redis = redis;
        _database = _redis.GetDatabase();
        _logger = logger;
    }

    public async Task PublishMessageAsync(
        string channel,
        string message,
        CancellationToken cancellationToken = default
    )
    {
        await _database.PublishAsync(channel, message);
    }

    public async Task ConsumeMessageAsync(
        string channel,
        CancellationToken cancellationToken = default
    )
    {
        var subscriber = _redis.GetSubscriber();

        await subscriber.SubscribeAsync(
            channel,
            (channel, message) =>
            {
                _logger.LogInformation(message);
            }
        );
    }
}
