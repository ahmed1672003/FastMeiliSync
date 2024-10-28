using System.Text;
using Dapper;
using FastMeiliSync.Application.Abstractions;
using FastMeiliSync.Infrastructure.Postgres.Models;
using FastMeiliSync.Infrastructure.Redis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using Npgsql.Replication;
using Npgsql.Replication.Internal;

namespace FastMeiliSync.Infrastructure.Postgres.Service;

public sealed class Wal2JosnService : IWal2JosnService
{
    readonly ILogger _logger;
    readonly IRedisService _redisService;

    public Wal2JosnService(ILogger<Wal2JosnService> logger, IRedisService redisService)
    {
        _logger = logger;
        _redisService = redisService;
    }

    public async Task StartReplicationConnectionAsync(
        string database,
        string databaseUrl,
        string meiliSearchUrl,
        CancellationToken cancellationToken = default
    )
    {
        await using (var connection = new LogicalReplicationConnection(databaseUrl))
        {
            try
            {
                var slotName = string.Concat(database, "_", "slot").ToLower();
                _logger.LogInformation($"{database} sync started....");

                var replicationOptions = new ReplicationSlotOptions(slotName);
                var replicationSlote = new NpgsqlReplicationSlot(
                    nameof(OutPutPlugins.wal2json),
                    replicationOptions
                );
                connection.WalReceiverTimeout = Timeout.InfiniteTimeSpan;

                await connection.Open();

                if (!await SlotExistAsync(databaseUrl, slotName))
                    await CreateRepplicatinSlot(databaseUrl, slotName);

                await foreach (
                    var changes in connection.StartLogicalReplication(
                        replicationSlote,
                        cancellationToken
                    )
                )
                {
                    await using (var memoryStream = new MemoryStream())
                    {
                        await changes.Data.CopyToAsync(memoryStream);
                        var messageContent = Encoding.UTF8.GetString(memoryStream.ToArray());
                        var postgresMessage = PostgresMessage.Create(
                            database,
                            messageContent,
                            meiliSearchUrl
                        );
                        await _redisService.PublishMessageAsync(
                            RedisConstants.DEFAULT_CHANNEL,
                            JsonConvert.SerializeObject(postgresMessage)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }

    async Task<bool> SlotExistAsync(string url, string slotName)
    {
        using (var connection = new NpgsqlConnection(url))
        {
            try
            {
                return await connection.QuerySingleAsync<bool>(
                    $@"
                    select exists (
                    select * from pg_catalog.pg_replication_slots where  slot_name = '{slotName}'
                    );  
                "
                );
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }

    async Task CreateRepplicatinSlot(string url, string slotName)
    {
        using (var connection = new NpgsqlConnection(url))
        {
            try
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText =
                    @$"
                    select  pg_create_logical_replication_slot('{slotName}','{nameof(OutPutPlugins.wal2json)}');
                    ";
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
