namespace FastMeiliSync.Infrastructure.SearchEngine.Document;

public interface IDocumentService
{
    Task<TaskInfo> AddRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string indexKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    );

    TaskInfo AddRangeAsJson(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string indexKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY
    );

    Task<TaskInfo> UpdateRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string indexKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    );

    Task<TaskInfo> RemoveRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        IEnumerable<string> documentIds,
        string indexName,
        string documentKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    );
}
