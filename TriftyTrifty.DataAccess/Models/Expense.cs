using System.ComponentModel.DataAnnotations;

namespace TriftyTrifty.DataAccess.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required] public string Description { get; set; } = null!;
        [Required] public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required] public string PaidByUserId { get; set; } = null!;

        public AppUser? PaidByUser { get; set; }

        public int GroupId { get; set; }
        public ExpenseGroup? Group { get; set; }
    }

}
