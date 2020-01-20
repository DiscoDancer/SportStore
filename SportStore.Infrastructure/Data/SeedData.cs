using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Core.Entities;

namespace SportStore.Infrastructure.Data
{
    public static class SeedData
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Secret123$";

        public static void Initialize(IServiceProvider serviceProvider)
        {
            EnsureIdentityPopulated(serviceProvider.GetRequiredService<UserManager<IdentityUser>>());
            EnsurePopulated(new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()));
        }

        /**
         * Another options to do (outside of migrations) is following:
         * context.Database.EnsureCreated();
         */
        private static void EnsurePopulated(AppDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            PopulateTestData(context);
        }

        private static async void EnsureIdentityPopulated(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(AdminUser);
            if (user != null)
            {
                return;
            }
            user = new IdentityUser(AdminUser);
            await userManager.CreateAsync(user, AdminPassword);
        }

        private static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Products)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            dbContext.Products.AddRange(
                new Product
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    Category = "Watersports",
                    Price = 275
                },
                new Product
                {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    Category = "Watersports",
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    Category = "Soccer",
                    Price = 19.50m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Category = "Soccer",
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    Category = "Soccer",
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Category = "Chess",
                    Price = 16
                },
                new Product
                {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Category = "Chess",
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    Category = "Chess",
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    Category = "Chess",
                    Price = 1200
                });

            dbContext.SaveChanges();
        }
    }
}
