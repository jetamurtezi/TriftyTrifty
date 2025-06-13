using Microsoft.AspNetCore.Mvc;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Controllers
{
    public class ExpenseGroupController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseGroupController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var groups = _context.ExpenseGroups.ToList();
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
                _context.ExpenseGroups.Add(group);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        public IActionResult Delete(int id)
        {
            var group = _context.ExpenseGroups.FirstOrDefault(e => e.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var group = _context.ExpenseGroups.FirstOrDefault(e => e.Id == id);
            if (group != null)
            {
                _context.ExpenseGroups.Remove(group);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
