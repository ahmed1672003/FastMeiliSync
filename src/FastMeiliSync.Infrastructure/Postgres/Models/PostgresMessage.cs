namespace FastMeiliSync.Infrastructure.Postgres.Models;

public class PostgresMessage
{
    private PostgresMessage(string database, string content, string meiliSearchUrl)
    {
        Database = database;
        Content = content;
        MeiliSearchUrl = meiliSearchUrl;
    }

    public string Database { get; private set; }
    public string Content { get; private set; }
    public string MeiliSearchUrl { get; set; }

    public static PostgresMessage Create(string database, string content, string meiliSearchUrl) =>
        new(database, content, meiliSearchUrl);
}
