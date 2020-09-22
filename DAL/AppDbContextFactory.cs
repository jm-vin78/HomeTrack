using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContextFactory
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContextFactory(DatabaseConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(configuration.ConnectionString);

            _options = builder.Options;
        }

        public AppDbContext CreateContext()
        {
            return new AppDbContext(_options);
        }
    }
}
