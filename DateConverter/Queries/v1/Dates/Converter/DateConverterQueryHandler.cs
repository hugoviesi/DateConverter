using DateConverter.CrossCutting.Configuration;
using DateConverter.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DateConverter.Domain.Queries.v1.Dates.Converter;

public sealed class DateConverterQueryHandler : IRequestHandler<DateConverterQuery, DateConverterQueryResponse>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger _logger;

    private readonly AppSettings _appSettings = new();

    public DateConverterQueryHandler(IMemoryCache memoryCache, ILoggerFactory loggerFactory)
    {
        _memoryCache = memoryCache;
        _logger = loggerFactory.CreateLogger<DateConverterQueryHandler>();
    }

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

        var dateFactory = DateFactory.Create(request.Day);

        var date = dateFactory.IsTurnedMonth()
            ? dateFactory.GetDateWithTurnOfMounth()
            : dateFactory.GetDate();

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_appSettings.CacheSettings.AbsoluteExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(_appSettings.CacheSettings.SlidingExpirationInMinutes)
        };

        _memoryCache.Set(cacheKey, date, cacheEntryOptions);

        _logger.LogInformation("Date computed and cached for key: {CacheKey} | {CorrelationId}", cacheKey, correlationId);   

        return new DateConverterQueryResponse
        {
            Date = date
        };
    }
}
