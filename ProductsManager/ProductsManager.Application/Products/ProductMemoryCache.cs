using Microsoft.Extensions.Caching.Memory;
using ProductsManager.Application.Abstractions.MemoryCache;

namespace ProductsManager.Application.Products
{
    public class ProductMemoryCache(IMemoryCache memoryCache) : IProductMemoryCache
    {              
        void InitStatusCache()
        {
            Dictionary<int, string> productsStatus =
                new()
                {
                        { 0, "Inactive" },
                        { 1, "Active" }
                };

            if (!memoryCache.TryGetValue("ProductsStatus", out Dictionary<int, string> productsStatusCache))
            {
                productsStatusCache = productsStatus;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                memoryCache.Set("ProductsStatus", productsStatusCache, cacheEntryOptions);
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
