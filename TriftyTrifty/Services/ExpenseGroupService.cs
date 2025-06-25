using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Services
{
    public class ExpenseGroupService : IExpenseGroupService
    {
        private readonly IExpenseGroupRepository _groupRepo; 

        public ExpenseGroupService(IExpenseGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        public IEnumerable<ExpenseGroup> GetAllExpenseGroups()
        {
            return _groupRepo.GetAll();
        }

        public ExpenseGroup GetByIdWithExpenses(int id)
        {
            return _groupRepo.GetByIdWithExpenses(id);
        }

        public ExpenseGroup GetExpenseGroupById(int id)
        {
            return _groupRepo.GetById(id);
        }
        public void AddExpenseGroup(ExpenseGroup group)
        {
            _groupRepo.Add(group);

        }
        public void DeleteExpenseGroup(int id)
        {
            _groupRepo.Delete(id);
        }
        public void Save()
        {
            _groupRepo.Save();
        }
    }
}
