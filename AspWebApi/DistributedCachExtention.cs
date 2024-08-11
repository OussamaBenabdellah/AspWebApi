using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AspWebApi
{
    public static class DistributedCachExtention
    {
        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key)
            where T : class //pour confirmer la serialisation ce fait pour une class
        {
            var json = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(json)) return default; 
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T Value)
            where T : class 
        {
            var json = JsonSerializer.Serialize(cache);
            await cache.SetStringAsync(key, json);
        }
    }
}
