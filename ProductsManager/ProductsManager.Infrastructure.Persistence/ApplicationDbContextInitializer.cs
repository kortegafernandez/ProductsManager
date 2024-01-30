using Microsoft.EntityFrameworkCore;

namespace ProductsManager.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
