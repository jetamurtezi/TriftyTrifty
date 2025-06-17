using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TriftyTrifty.DataAccess;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IExpenseGroupRepository _groupRepo;

        public ExpenseController(IExpenseRepository expenseRepo, IUserRepository userRepo, IExpenseGroupRepository groupRepo)
        {
            _expenseRepo = expenseRepo;
            _userRepo = userRepo;
            _groupRepo = groupRepo;
        }
        public IActionResult Index()
        {
            var expenses = _expenseRepo.GetAllWithUserOrdered();
            return View(expenses);
        }
        public IActionResult Create(int groupId)
        {
            LoadUsersDropdown();

            var expense = new Expense
            {
                GroupId = groupId,
                Date = DateTime.Today
            };

            return View("CreateOrEdit", expense);
        }


        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseRepo.Add(expense);
                _expenseRepo.Save();
                return RedirectToAction("Index");
            }

            LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }

        public IActionResult ByGroup(int groupId)
        {
            var group = _groupRepo.GetByIdWithExpenses(groupId);

            if (group == null) { 
                return NotFound();
            }
            ViewData["GroupId"] = group.Id;
            ViewData["GroupName"] = group.GroupName;

            return View(group.Expenses.ToList()); 
        }


        public IActionResult Edit(int id)
        {
            var expense = _expenseRepo.GetById(id);
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
                _expenseRepo.Add(expense);
            }
            else
            {
                _expenseRepo.Update(expense);
            }
            _expenseRepo.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var expense = _expenseRepo.GetWithUser(id);
            if (expense == null) {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed (int id)
        {
            var expense= _expenseRepo.GetById(id);
            if (expense!=null) {
                _expenseRepo.Delete(id);
                _expenseRepo.Save();
            }
            return RedirectToAction("Index");
        }


        private void LoadUsersDropdown(int? selectedUserId = null)
        {
            var users = _userRepo.GetAll();
            ViewData["Users"] = new SelectList(users, "Id", "Name", selectedUserId);
        }
    }
}
