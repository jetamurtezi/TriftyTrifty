using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IUserService _userService;
        private readonly IExpenseGroupService _groupService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBalanceService _balanceService;

        public ExpenseController(
            IExpenseService expenseService,
            IUserService userService,
            IExpenseGroupService groupService,
            UserManager<AppUser> userManager,
            IBalanceService balanceService)
        {
            _expenseService = expenseService;
            _userService = userService;
            _groupService = groupService;
            _userManager = userManager;
            _balanceService= balanceService;
        }

        public IActionResult Index()
        {
            var expenses = _expenseService.GetAllWithUserOrdered();
            return View(expenses);
        }

        public async Task<IActionResult> Create(int groupId)
        {
            await LoadUsersDropdown();

            var expense = new Expense
            {
                GroupId = groupId,
                Date = DateTime.Today
            };

            return View("CreateOrEdit", expense);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseService.AddExpense(expense);
                return RedirectToAction("ByGroup", new { groupId = expense.GroupId });
            }

            await LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }



        public IActionResult ByGroup(int groupId)
        {
            var group = _groupService.GetByIdWithExpenses(groupId);

            if (group == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = group.Id;
            ViewData["GroupName"] = group.GroupName;

            return View(group.Expenses.ToList());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = _expenseService.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }

            await LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }

        
        [HttpPost]
        public async Task<IActionResult> Save(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                await LoadUsersDropdown(expense.PaidByUserId);
                return View("CreateOrEdit", expense);
            }

            if (expense.Id == 0)
                _expenseService.AddExpense(expense);
            else
                _expenseService.UpdateExpense(expense);

            return RedirectToAction("ByGroup", new { groupId = expense.GroupId });
        }

        public IActionResult Delete(int id)
        {
            var expense = _expenseService.GetUserById(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var expense = _expenseService.GetById(id);
            if (expense == null) { return NotFound(); }
            else
            {
                var groupId = expense.GroupId;
                _expenseService.DeleteExpense(id);
                return RedirectToAction("ByGroup", new { groupId = groupId });
            }
        }

        private async Task LoadUsersDropdown(string? selectedUserId = null)
        {
            var usersInUserRole = await _userManager.GetUsersInRoleAsync("User");
            ViewData["Users"] = new SelectList(usersInUserRole, "Id", "Name", selectedUserId);
        }

        public IActionResult Balances(int groupId)
        {
            var group = _groupService.GetByIdWithExpenses(groupId);
            if (group == null) return NotFound();

            var transactions = _balanceService.CalculateBalances(group);

            ViewData["GroupName"] = group.GroupName;
            ViewData["Transactions"] = transactions;

            return View();
        }



    }
}
