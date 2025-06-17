using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Repositories.IRepositories
{
    public interface IExpenseGroupRepository
    {
        IEnumerable<ExpenseGroup> GetAll();
        ExpenseGroup GetById(int id);
        ExpenseGroup GetByIdWithExpenses(int id);
        void Add(ExpenseGroup group);
        void Delete(int id);
        void Save();
    }

}
