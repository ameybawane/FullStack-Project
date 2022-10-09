using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }

        // create property of type dbset
        public DbSet<Employee> Employees { get; set; }
        // using this property we can access and communicate with this Employee table
    }
}
