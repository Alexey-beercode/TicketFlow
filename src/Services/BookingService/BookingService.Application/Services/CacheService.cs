using System.Text.Json;
using BookingService.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BookingService.Application.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly ILogger<CacheService> _logger;
    
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public CacheService(IConnectionMultiplexer redis, ILogger<CacheService> logger)
    {
        _logger = logger;
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        _logger.LogDebug("Getting value for key: {Key}", key);
        
        var value = await _db.StringGetAsync(key);
        if (!value.HasValue)
        {
            return default;
        }
        
        return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var serializedValue = JsonSerializer.Serialize(value, _jsonOptions);
        await _db.StringSetAsync(key, serializedValue, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }
}