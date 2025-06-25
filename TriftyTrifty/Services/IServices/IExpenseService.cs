using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Services.IServices
{
    public interface IExpenseService
    {
        List<Expense> GetAllByGroup(int groupId);
        Expense GetById(int id);
        Expense GetUserById(int id);
        List<Expense> GetAllWithUserOrdered();
        void AddExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int id);
    }
}
