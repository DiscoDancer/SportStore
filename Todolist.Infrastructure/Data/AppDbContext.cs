using Microsoft.EntityFrameworkCore;
using Todolist.Core.Entities;

namespace Todolist.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> getRequiredService, DbContextOptions options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}