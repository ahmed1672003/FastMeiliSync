namespace FastMeiliSync.Infrastructure.Context.Configurations;

internal sealed class MeiliSearchConfiguration : IEntityTypeConfiguration<MeiliSearch>
{
    public void Configure(EntityTypeBuilder<MeiliSearch> builder)
    {
        builder.ToTable(nameof(Tables.MeiliSearche), nameof(Schemas.Public));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ApiKey).IsRequired(true);
        builder.Property(x => x.Url).IsRequired(true);
        builder.Property(x => x.Label).IsRequired(true);
        builder.HasIndex(x => x.Label).IsUnique(true);
        builder.HasIndex(x => x.Url).IsUnique(true);
    }
}
