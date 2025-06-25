using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Services.IServices
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetAllUsers();
        AppUser GetUserById(string id);    
        void Add (AppUser user);
        void Update (AppUser user);
        void Delete (string id);
        void Save();
    }
}
