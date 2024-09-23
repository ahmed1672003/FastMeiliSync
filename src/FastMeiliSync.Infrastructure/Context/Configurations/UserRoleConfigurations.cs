namespace FastMeiliSync.Infrastructure.Context.Configurations;

public sealed class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(Tables.UserRole), nameof(Schemas.Public));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.RoleId, x.UserId }).IsUnique(true);
    }
}
