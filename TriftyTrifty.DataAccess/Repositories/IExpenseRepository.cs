using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Repositories
{
    internal interface IExpenseRepository
    {
        IEnumerable<Expense> GetAllExpenses();
        Expense GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(int id);
        void Save();
    }
}
