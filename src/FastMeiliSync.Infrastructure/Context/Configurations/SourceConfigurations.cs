namespace FastMeiliSync.Infrastructure.Context.Configurations;

public class SourceConfigurations : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable(nameof(Tables.Source), nameof(Schemas.Public));
        builder.Property(x => x.Database).IsRequired(false);
        builder.Property(x => x.Url).IsRequired(false);
        builder.HasIndex(x => x.Label).IsUnique(true);
        builder.HasIndex(x => x.Url).IsUnique(true);
        builder.HasIndex(x => x.Database).IsUnique(true);
        //  builder.HasQueryFilter(x => !x.Deleted);
    }
}
