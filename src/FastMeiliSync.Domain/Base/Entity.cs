namespace FastMeiliSync.Domain.Base;

public record Entity<T>
    where T : notnull
{
    public T Id { get; protected set; }
}
