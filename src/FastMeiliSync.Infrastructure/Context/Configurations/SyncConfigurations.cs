namespace FastMeiliSync.Infrastructure.Context.Configurations;

internal sealed class SyncConfigurations : IEntityTypeConfiguration<Sync>
{
    public void Configure(EntityTypeBuilder<Sync> builder)
    {
        builder.ToTable(nameof(Tables.Sync), nameof(Schemas.Public));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Label).IsRequired(true);
        builder.Property(x => x.SourceId).IsRequired(true);
        builder.Property(x => x.SourceId).IsRequired(true);
        builder.HasIndex(x => new { x.SourceId, x.MeiliSearchId }).IsUnique(true);
        builder.HasIndex(x => x.Label).IsUnique(true);
        builder.HasQueryFilter(x => !x.Deleted);
    }
}
