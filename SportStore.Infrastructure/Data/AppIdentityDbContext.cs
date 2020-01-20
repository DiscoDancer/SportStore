using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Infrastructure.Data
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        // if you replace DbContextOptions<AppIdentityDbContext> -> DbContextOptions app will crash
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }
    }
}