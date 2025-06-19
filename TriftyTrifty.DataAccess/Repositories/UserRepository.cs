using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AppUser> GetAll() => _context.Users.ToList();

        public AppUser GetById(string id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public void Add(AppUser user) => _context.Users.Add(user);

        public void Update(AppUser user) => _context.Users.Update(user);

        public void Delete(string id)
        {
            var user = GetById(id);
            if (user != null) _context.Users.Remove(user);
        }

        public void Save() => _context.SaveChanges();
    }
}
