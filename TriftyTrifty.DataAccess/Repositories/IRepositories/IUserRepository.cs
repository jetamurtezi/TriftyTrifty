using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.DataAccess.Repositories.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<AppUser> GetAll();
        AppUser GetById(string id);
        void Add(AppUser user);
        void Update(AppUser user);
        void Delete(string id);
        void Save();
    }
}
