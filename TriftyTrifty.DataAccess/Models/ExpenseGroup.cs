using System.ComponentModel.DataAnnotations;

namespace TriftyTrifty.DataAccess.Models
{
    public class ExpenseGroup
    {
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    }
}
