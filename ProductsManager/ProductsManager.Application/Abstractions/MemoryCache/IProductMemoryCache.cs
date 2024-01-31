namespace ProductsManager.Application.Abstractions.MemoryCache
{
    public interface IProductMemoryCache
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    }
}
