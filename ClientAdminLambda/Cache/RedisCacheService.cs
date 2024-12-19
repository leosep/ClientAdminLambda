using StackExchange.Redis;

namespace ClientAdminLambda.Cache;

public class RedisCacheService
{
    private readonly IDatabase _cache;

    public RedisCacheService(string redisConnectionString)
    {
        var connection = ConnectionMultiplexer.Connect(redisConnectionString);
        _cache = connection.GetDatabase();
    }

    public async Task<string?> GetAsync(string key) => await _cache.StringGetAsync(key);

    public async Task SetAsync(string key, string value, TimeSpan expiration) =>
        await _cache.StringSetAsync(key, value, expiration);
}
