﻿using Microsoft.EntityFrameworkCore;
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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Expense> GetAll() =>
            _context.Expenses.Include(e => e.PaidByUser).OrderByDescending(e => e.Date).ToList();

        public IEnumerable<Expense> GetByGroupId(int groupId) =>
            _context.Expenses.Include(e => e.PaidByUser).Where(e => e.GroupId == groupId).ToList();

        public Expense GetById(int id) => _context.Expenses.FirstOrDefault(e => e.Id == id);
        public List<Expense> GetAllWithUserOrdered()
        {
            return _context.Expenses
                .Include(e => e.PaidByUser)
                .OrderByDescending(e => e.Date)
                .ToList();
        }
        public Expense GetWithUser(int id)
        {
            return _context.Expenses.Include(e => e.PaidByUser).FirstOrDefault(e => e.Id == id);
        }
        public void Add(Expense expense) => _context.Expenses.Add(expense);

        public void Update(Expense expense) => _context.Expenses.Update(expense);

        public void Delete(int id)
        {
            var expense = GetById(id);
            if (expense != null) _context.Expenses.Remove(expense);
        }

        public void Save() => _context.SaveChanges();
    }



}
