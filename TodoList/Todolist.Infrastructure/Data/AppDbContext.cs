using Microsoft.EntityFrameworkCore;
using TodoList.Core.Entities;

namespace TodoList.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}