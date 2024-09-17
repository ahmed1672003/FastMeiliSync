namespace FastMeiliSync.Infrastructure.SearchEngine.Document;

public sealed class DocumentService(IHttpClientFactory httpClientFactory) : IDocumentService
{
    readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<TaskInfo> AddRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string documentKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    )
    {
        using (var _httpClient = _httpClientFactory.CreateClient())
        {
            _httpClient.BaseAddress = new Uri(meiliSearchUrl);
            var meiliSearchClient = new MeilisearchClient(_httpClient, apiKey);
            var index = await meiliSearchClient.GetIndexAsync(indexName, cancellationToken);
            return await index.AddDocumentsJsonAsync(documents, documentKey, cancellationToken);
        }
    }

    public TaskInfo AddRangeAsJson(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string documentKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY
    )
    {
        using (var _httpClient = _httpClientFactory.CreateClient())
        {
            _httpClient.BaseAddress = new Uri(meiliSearchUrl);
            var meiliSearchClient = new MeilisearchClient(_httpClient, apiKey);

            var index = meiliSearchClient
                .GetIndexAsync(indexName)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return index
                .AddDocumentsJsonAsync(documents, documentKey)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }

    public async Task<TaskInfo> UpdateRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        string documents,
        string indexName,
        string documentKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    )
    {
        using (var _httpClient = _httpClientFactory.CreateClient())
        {
            _httpClient.BaseAddress = new Uri(meiliSearchUrl);
            var meiliSearchClient = new MeilisearchClient(_httpClient, apiKey);
            var index = await meiliSearchClient.GetIndexAsync(indexName, cancellationToken);
            return await index.UpdateDocumentsJsonAsync(documents, documentKey, cancellationToken);
        }
    }

    public async Task<TaskInfo> RemoveRangeAsJsonAsync(
        string meiliSearchUrl,
        string apiKey,
        IEnumerable<string> documentIds,
        string indexName,
        string documentKey = MeiliSearchConstants.DEFAULT_DOCUMENT_KEY,
        CancellationToken cancellationToken = default
    )
    {
        using (var _httpClient = _httpClientFactory.CreateClient())
        {
            _httpClient.BaseAddress = new Uri(meiliSearchUrl);
            var meiliSearchClient = new MeilisearchClient(_httpClient, apiKey);
            var index = await meiliSearchClient.GetIndexAsync(indexName);
            return await index.DeleteDocumentsAsync(documentIds, cancellationToken);
        }
    }
}
