using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Controllers
{
    [Authorize] 
    public class ExpenseGroupController : Controller
    {
        private readonly IExpenseGroupService _groupService;

        public ExpenseGroupController(IExpenseGroupService groupService)
        {
            _groupService = groupService;
        }

        public IActionResult Index()
        {
            var groups = _groupService.GetAllExpenseGroups();
            return View(groups);
        }

        
        public IActionResult Create()
        {
            return View(new ExpenseGroup());
        }

        [HttpPost]
        
        public IActionResult Create(ExpenseGroup group)
        {
            if (ModelState.IsValid)
            {
                _groupService.AddExpenseGroup(group);
                _groupService.Save();
                return RedirectToAction("Index", "Home");
            }

            return View(group);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var group = _groupService.GetExpenseGroupById(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var group = _groupService.GetExpenseGroupById(id);
            if (group != null)
            {
                _groupService.DeleteExpenseGroup(id);
                _groupService.Save();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
