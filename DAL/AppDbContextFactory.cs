using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class AppDbContextFactory
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContextFactory()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql("Server=;Port=5432;Database=;User Id=;Password=;");

            _options = builder.Options;
        }

        public AppDbContext CreateContext()
        {
            return new AppDbContext(_options);
        }
    }
}
