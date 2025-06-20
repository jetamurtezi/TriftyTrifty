using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty.Controllers
{
    [Authorize] 
    public class ExpenseGroupController : Controller
    {
        private readonly IExpenseGroupRepository _groupRepository;

        public ExpenseGroupController(IExpenseGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IActionResult Index()
        {
            var groups = _groupRepository.GetAll();
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
                _groupRepository.Add(group);
                _groupRepository.Save();
                return RedirectToAction("Index", "Home");
            }

            return View(group);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var group = _groupRepository.GetById(id);
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
            var group = _groupRepository.GetById(id);
            if (group != null)
            {
                _groupRepository.Delete(id);
                _groupRepository.Save();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
