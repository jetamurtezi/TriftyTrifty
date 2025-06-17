using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty.DataAccess.Repositories
{
    public class ExpenseGroupRepository : IExpenseGroupRepository
    {
        private readonly AppDbContext _context;

        public ExpenseGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ExpenseGroup> GetAll() => _context.ExpenseGroups.ToList();

        public ExpenseGroup GetById(int id) => _context.ExpenseGroups.FirstOrDefault(g => g.Id == id);
        public ExpenseGroup GetByIdWithExpenses(int id)
        {
            return _context.ExpenseGroups.Include(g=>g.Expenses).ThenInclude(e=>e.PaidByUser).FirstOrDefault(g=>g.Id==id);
        }

        public void Add(ExpenseGroup group) => _context.ExpenseGroups.Add(group);

        public void Delete(int id)
        {
            var group = GetById(id);
            if (group != null) _context.ExpenseGroups.Remove(group);
        }

        public void Save() => _context.SaveChanges();
    }

}
