using Microsoft.Extensions.Caching.Memory;
using ProductsManager.Application.Abstractions.MemoryCache;
using ProductsManager.Application.Constants;

namespace ProductsManager.Infrastructure.Shared.MemoryCache
{
    public class ProductMemoryCache(IMemoryCache memoryCache) : IProductMemoryCache
    {              
        void InitStatusCache()
        {
            if (!memoryCache.TryGetValue(Constants.PRODUCT_CACHE_STATUS_KEY, out Dictionary<int, string> productsStatusCache))
            {
                productsStatusCache =
                    new()
                {
                        { 0, "Inactive" },
                        { 1, "Active" }
                }; ;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(Constants.PRODUCT_CACHE_EXPIRATION_MINUTES));

                memoryCache.Set(Constants.PRODUCT_CACHE_STATUS_KEY, productsStatusCache, cacheEntryOptions);
            }
        }

        public T GetData<T>(string key)
        {
            InitStatusCache();

            T item = (T)memoryCache.Get(key);
            return item;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {           
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    memoryCache.Set(key, value, expirationTime);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return res;
        }
    }
}
