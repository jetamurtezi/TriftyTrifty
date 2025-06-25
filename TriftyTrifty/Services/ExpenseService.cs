using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public void AddExpense(Expense expense)
        {
            _expenseRepository.Add(expense);
            _expenseRepository.Save();
        }
        public void UpdateExpense(Expense expense)
        {
            _expenseRepository.Update(expense);
            _expenseRepository.Save();
        }
        public void DeleteExpense(int id)
        {
            _expenseRepository.Delete(id);
            _expenseRepository.Save();
        }

        public List<Expense> GetAllByGroup(int groupId)
        {
            return _expenseRepository.GetByGroupId(groupId).ToList();
        }

        public List<Expense> GetAllWithUserOrdered()
        {
            return _expenseRepository.GetAllWithUserOrdered().ToList();
        }

        public Expense GetById(int id)
        {
            return _expenseRepository.GetById(id);
        }

        public Expense GetUserById(int id)
        {
            return _expenseRepository.GetWithUser(id);
        }

        
    }
}
