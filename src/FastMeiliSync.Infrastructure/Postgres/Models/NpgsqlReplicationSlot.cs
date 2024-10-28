using Npgsql.Replication;
using Npgsql.Replication.Internal;

namespace FastMeiliSync.Infrastructure.Postgres.Models;

public sealed class NpgsqlReplicationSlot : LogicalReplicationSlot
{
    public NpgsqlReplicationSlot(string outputPlugin, ReplicationSlotOptions replicationSlotOptions)
        : base(outputPlugin, replicationSlotOptions) { }
}
