using Bonbonniere.Core.Models;
using Bonbonniere.Services.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Interfaces
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
