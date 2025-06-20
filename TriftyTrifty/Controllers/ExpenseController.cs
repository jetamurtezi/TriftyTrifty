using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IExpenseGroupRepository _groupRepo;
        private readonly UserManager<AppUser> _userManager;

        public ExpenseController(
            IExpenseRepository expenseRepo,
            IUserRepository userRepo,
            IExpenseGroupRepository groupRepo,
            UserManager<AppUser> userManager)
        {
            _expenseRepo = expenseRepo;
            _userRepo = userRepo;
            _groupRepo = groupRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var expenses = _expenseRepo.GetAllWithUserOrdered();
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
                _expenseRepo.Add(expense);
                _expenseRepo.Save();
                return RedirectToAction("ByGroup", new { groupId = expense.GroupId });
            }

            await LoadUsersDropdown(expense.PaidByUserId);
            return View("CreateOrEdit", expense);
        }



        public IActionResult ByGroup(int groupId)
        {
            var group = _groupRepo.GetByIdWithExpenses(groupId);

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
            var expense = _expenseRepo.GetById(id);
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
                _expenseRepo.Add(expense);
            else
                _expenseRepo.Update(expense);

            _expenseRepo.Save();
            return RedirectToAction("ByGroup", new { groupId = expense.GroupId });
        }

        public IActionResult Delete(int id)
        {
            var expense = _expenseRepo.GetWithUser(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var expense = _expenseRepo.GetById(id);
            if (expense == null) { return NotFound(); }
            else
            {
                var groupId = expense.GroupId;
                _expenseRepo.Delete(id);
                _expenseRepo.Save();
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
            var group = _groupRepo.GetByIdWithExpenses(groupId);
            if (group == null) return NotFound();

            var expenses = group.Expenses;
            var users = expenses.Select(e => e.PaidByUser).Distinct().ToList();
            var total = expenses.Sum(e => e.Amount);
            var perPerson = users.Count == 0 ? 0 : total / users.Count;

            // Bilancet për secilin user
            var balances = users.Select(u => new
            {
                User = u,
                Balance = expenses.Where(e => e.PaidByUserId == u.Id).Sum(e => e.Amount) - perPerson
            }).ToList();

            // Lista e atyre që kanë paguar më shumë
            var creditors = balances.Where(b => b.Balance > 0).OrderByDescending(b => b.Balance).ToList();
            // Lista e atyre që kanë paguar më pak
            var debtors = balances.Where(b => b.Balance < 0).OrderBy(b => b.Balance).ToList();

            var transactions = new List<string>();

            int i = 0, j = 0;
            while (i < debtors.Count && j < creditors.Count)
            {
                var debtor = debtors[i];
                var creditor = creditors[j];

                var amount = Math.Min(-debtor.Balance, creditor.Balance);

                transactions.Add($"{debtor.User.Name} duhet t’i japë {creditor.User.Name} {amount:0.00}€");

                // Përditëso bilancet
                debtor = new { debtor.User, Balance = debtor.Balance + amount };
                creditor = new { creditor.User, Balance = creditor.Balance - amount };

                if (Math.Abs(debtor.Balance) < 0.01m) i++;
                else debtors[i] = debtor;

                if (creditor.Balance < 0.01m) j++;
                else creditors[j] = creditor;
            }

            ViewData["GroupName"] = group.GroupName;
            ViewData["Transactions"] = transactions;

            return View(); 
        }



    }
}
