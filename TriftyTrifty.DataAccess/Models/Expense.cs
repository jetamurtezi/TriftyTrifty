using System.ComponentModel.DataAnnotations;

namespace TriftyTrifty.DataAccess.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int PaidByUserId { get; set; }

        [Required]
        public AppUser PaidByUser { get; set; }

        public int GroupId { get; set; }
        public ExpenseGroup Group { get; set; }
    }
}
