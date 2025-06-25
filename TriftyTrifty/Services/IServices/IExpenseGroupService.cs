using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Services.IServices
{
    public interface IExpenseGroupService
    {
        IEnumerable<ExpenseGroup> GetAllExpenseGroups();
        ExpenseGroup GetExpenseGroupById(int id);
        ExpenseGroup GetByIdWithExpenses (int id);
        void AddExpenseGroup(ExpenseGroup group);
        void DeleteExpenseGroup(int id);
        void Save();

    }
}
