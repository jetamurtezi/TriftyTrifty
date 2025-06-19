using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext <AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
