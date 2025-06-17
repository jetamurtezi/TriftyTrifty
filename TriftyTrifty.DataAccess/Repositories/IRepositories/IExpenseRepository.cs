using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Repositories.IRepositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetAll();
        IEnumerable<Expense> GetByGroupId(int groupId);
        Expense GetById(int id);
        List<Expense> GetAllWithUserOrdered();
        Expense GetWithUser(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(int id);
        void Save();
    }
}
