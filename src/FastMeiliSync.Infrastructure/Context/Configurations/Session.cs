using FastMeiliSync.Domain.Entities;

namespace FastMeiliSync.Infrastructure.Context.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
