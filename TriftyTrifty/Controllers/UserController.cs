using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users); 
        }

        public IActionResult Create()
        {
            return View(new AppUser());
        }

        [HttpPost]
        public IActionResult Create(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _userService.Add(user);
                _userService.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(string id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(string id)
        {
            _userService.Delete(id);
            _userService.Save();
            return RedirectToAction("Index");
        }
    }
}
