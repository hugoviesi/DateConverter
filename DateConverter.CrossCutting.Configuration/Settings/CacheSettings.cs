using System.Text.Json.Serialization;

namespace DateConverter.CrossCutting.Configuration.Settings;

public sealed record CacheSettings
{
    [JsonPropertyName("AbsoluteExpirationInMinutes")]
    public int AbsoluteExpirationInMinutes { get; set; }

    [JsonPropertyName("SlidingExpirationInMinutes")]
    public int SlidingExpirationInMinutes { get; set; }
}
