using Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<users_db1>Users_db1 { get; set; }
    }
}