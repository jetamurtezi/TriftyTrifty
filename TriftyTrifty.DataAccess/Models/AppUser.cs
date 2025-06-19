using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TriftyTrifty.DataAccess.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Expense> ExpensesPaid { get; set; }
    }
}
