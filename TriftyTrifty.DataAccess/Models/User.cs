using System.ComponentModel.DataAnnotations;

namespace TriftyTrifty.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Expense> ExpensesPaid { get; set; }
    }
}
