namespace DateConverter.Domain.Queries.v1.Dates.Converter;

public sealed class DateConverterQueryHandler(IMemoryCache memoryCache, ILoggerFactory loggerFactory) : IRequestHandler<DateConverterQuery, DateConverterQueryResponse>
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly ILogger _logger = loggerFactory.CreateLogger<DateConverterQueryHandler>();

    private readonly AppSettings _appSettings = new();

    public async Task<DateConverterQueryResponse> Handle(DateConverterQuery request, CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid();

        var cacheKey = request.Day;

        if (_memoryCache.TryGetValue(cacheKey, out DateTime cachedDate))
        {
            _logger.LogInformation("Cache hit for key: {CacheKey} | {CorrelationId}", cacheKey, correlationId);

            return new DateConverterQueryResponse
            {
                Date = cachedDate
            };
        }

        _logger.LogInformation("Cache miss for key: {CacheKey}. Computing date... | {CorrelationId}", cacheKey, correlationId);

        var date = DateFactory
            .WithDay(request.Day)
            .GetCalculatedDate();

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_appSettings.CacheSettings.AbsoluteExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(_appSettings.CacheSettings.SlidingExpirationInMinutes)
        };

        _memoryCache.Set(cacheKey, date, cacheEntryOptions);

        _logger.LogInformation("Date computed and cached for key: {CacheKey} | {CorrelationId}", cacheKey, correlationId);

        var result = new DateConverterQueryResponse
        {
            Date = date
        };

        return await Task.FromResult(result);
    }
}
