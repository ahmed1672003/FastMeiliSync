using FastMeiliSync.Domain.Entities.Roles;

namespace FastMeiliSync.Infrastructure.Context.Configurations;

internal sealed class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Tables.Role), nameof(Schemas.Public));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}
