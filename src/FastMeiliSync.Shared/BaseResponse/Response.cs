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
