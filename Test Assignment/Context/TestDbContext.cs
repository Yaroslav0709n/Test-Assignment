using Microsoft.EntityFrameworkCore;
using Test_Assignment.Models;

namespace Test_Assignment.Context
{
    public class TestDbContext:DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
