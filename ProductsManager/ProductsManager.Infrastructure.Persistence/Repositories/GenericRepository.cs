using Microsoft.EntityFrameworkCore;
using ProductsManager.Application.Abstractions.Repositories;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IWriteGenericRepository<T>, IReadGenericRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FindAsync(id,cancellationToken);
        }

        public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            context.Set<T>().Add(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            T? entity = context.Set<T>().SingleOrDefault(s => s.Id == id);

            if (entity is null)
                return;

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
