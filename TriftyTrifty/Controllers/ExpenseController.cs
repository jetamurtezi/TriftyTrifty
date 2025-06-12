using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TriftyTrifty.DataAccess;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var expenses=_context.Expenses
                .Include(e => e.PaidByUser)
                .OrderByDescending(e => e.Date)
                .ToList();
            return View(expenses);
        }
        public IActionResult Create() 
        {
            LoadUsersDropdown();
            return View("CreateOrEdit", new Expense());
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }
        public IActionResult Edit(int id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound();
            }
            LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }

        [HttpPost]
        public IActionResult Save(Expense expense)
        {
            if (ModelState.IsValid) {
                LoadUsersDropdown(expense.PaidByUserId);
                return View("CreateOrEdit", expense);
            }
            if (expense.Id == 0) {
                _context.Expenses.Add(expense);
            }
            else
            {
                _context.Expenses.Update(expense);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var expense= _context.Expenses.
                Include(e=> e.PaidByUser)
                .FirstOrDefault(ex => ex.Id == id);
            if (expense == null) {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed (int id)
        {
            var expense=_context.Expenses.FirstOrDefault(e => e.Id == id);
            if (expense!=null) {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        private void LoadUsersDropdown(int? selectedUserId = null)
        {
            var users = _context.Users.ToList();
            ViewData["Users"] = new SelectList(users, "Id", "Name", selectedUserId);
        }
    }
}
