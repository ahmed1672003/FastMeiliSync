namespace FastMeiliSync.Infrastructure.Context.Configurations;

public class SourceConfigurations : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable(nameof(Tables.Source), nameof(Schemas.Public));
        builder.Property(x => x.Configurations).HasColumnType(nameof(NPGSQLTypes.jsonb));
        builder.Property(x => x.Host).IsRequired(false);
        builder.Property(x => x.Port).IsRequired(false);
        builder.Property(x => x.Database).IsRequired(false);
        builder.Property(x => x.Url).IsRequired(false);
        builder.HasQueryFilter(x => !x.Deleted);
    }
}
