namespace FastMeiliSync.Shared.BaseResponse;

public record Response
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyOrder(2)]
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }

    [JsonPropertyOrder(3)]
    [JsonPropertyName("message")]
    public string Message { get; set; }
}

public sealed record ResponseOf<TResult> : Response
{
    [JsonPropertyOrder(4)]
    [JsonPropertyName("result")]
    public TResult Result { get; set; }
}

public sealed record PagedResponseOf<TResult> : Response
{
    [JsonPropertyOrder(4)]
    public bool MoveNext => CurrentPage < TotalPages;

    [JsonPropertyOrder(5)]
    public bool MovePrevious => CurrentPage > 1;

    [JsonPropertyOrder(6)]
    public int PageSize { get; set; }

    [JsonPropertyOrder(7)]
    public int CurrentPage { get; set; }

    [JsonPropertyOrder(8)]
    public int TotalCount { get; set; }

    [JsonPropertyOrder(9)]
    public int TotalPages =>
        Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDecimal(PageSize)));

    [JsonPropertyOrder(10)]
    public IReadOnlyCollection<TResult> Items { get; set; }
}
