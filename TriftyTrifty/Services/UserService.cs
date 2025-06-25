using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories;
using TriftyTrifty.DataAccess.Repositories.IRepositories;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void Add(AppUser user)
        {
            _userRepo.Add(user);
        }

        public void Delete(string id)
        {
            _userRepo.Delete(id);
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
            return _userRepo.GetAll();
        }

        public AppUser GetUserById(string id)
        {
            return _userRepo.GetById(id);
        }

        public void Save()
        {
            _userRepo.Save();
        }

        public void Update(AppUser user)
        {
            _userRepo.Update(user);
        }
    }
}
