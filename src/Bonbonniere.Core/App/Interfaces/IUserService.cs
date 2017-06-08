using Bonbonniere.Core.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Core.App.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        User GetUser(string email);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        Task<SignInResult> PasswordSignInAsync(string username, string password);
    }
}
