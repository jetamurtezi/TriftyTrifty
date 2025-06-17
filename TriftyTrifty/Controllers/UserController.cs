using Microsoft.AspNetCore.Mvc;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var users= _userRepository.GetAll();
            return View();
        }

        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user=_userRepository.GetById(id);
            if(user==null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id) 
        { 
            _userRepository.Delete(id);
            _userRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
