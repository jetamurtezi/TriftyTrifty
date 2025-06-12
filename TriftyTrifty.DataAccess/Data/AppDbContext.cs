using Microsoft.EntityFrameworkCore;

using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
