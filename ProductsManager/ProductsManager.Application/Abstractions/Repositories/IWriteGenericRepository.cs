namespace ProductsManager.Application.Abstractions.Repositories
{
    public interface IWriteGenericRepository<T>
    {
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
