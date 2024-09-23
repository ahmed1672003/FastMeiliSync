using FastMeiliSync.Domain.Entities.Users;

namespace FastMeiliSync.Infrastructure.Context.Configurations;

public sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(Tables.User), nameof(Schemas.Public));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
    }
}
