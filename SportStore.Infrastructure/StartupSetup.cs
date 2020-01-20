using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=database.sqlite")); // will be created in web project root
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite("Data Source=identity-database.sqlite"));
        }

        public static void InitialiseDatabases(IServiceProvider services)
        {
            services.GetService<AppDbContext>().Database.Migrate();
            services.GetService<AppIdentityDbContext>().Database.Migrate();

            SeedData.Initialize(services);
        }
    }
}