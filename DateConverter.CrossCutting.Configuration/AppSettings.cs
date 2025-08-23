using DateConverter.CrossCutting.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace DateConverter.CrossCutting.Configuration;

public class AppSettings
{
    private readonly IConfiguration _configuration;

    [JsonPropertyName("CacheSettings")]
    public CacheSettings CacheSettings { get; set; }

    public AppSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();

        CacheSettings = SetCacheSettings();
    }

    private CacheSettings SetCacheSettings()
    {
        var cacheSettings = new CacheSettings();

        _configuration
            .GetSection("CacheSettings")
            .Bind(cacheSettings);

        return cacheSettings;
    }
}
