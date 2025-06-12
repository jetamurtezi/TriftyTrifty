namespace TriftyTrifty.DataAccess.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int PaidByUserId { get; set; }
        public User PaidByUser { get; set; }

        public int GroupId { get; set; }
        public ExpenseGroup Group { get; set; }
    }
}
