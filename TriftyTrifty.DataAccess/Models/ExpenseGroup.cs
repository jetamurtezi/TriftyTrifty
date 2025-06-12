namespace TriftyTrifty.DataAccess.Models
{
    public class ExpenseGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<User>Members { get; set; }
        public ICollection<Expense>Expenses { get; set; }
    }
}
