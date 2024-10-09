namespace FastMeiliSync.Domain.Entities
{
    public record Session : Entity<Guid>
    {
        public Session()
        {
            Id = Guid.NewGuid();
        }

        public string IpAddress { get; set; }
        public bool Active { get; set; }
        public string ConnectionId { get; set; }
    }
}
