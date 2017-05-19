using Bonbonniere.Core.Models;
using System.Collections.Generic;

namespace Bonbonniere.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
