using Bonbonniere.Core.Models;
using Bonbonniere.Services.Interfaces;
using System.Collections.Generic;
using Bonbonniere.Services.ServiceModels;
using System.Threading.Tasks;
using Bonbonniere.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bonbonniere.Services.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IDataProvider dataProvider) : base(dataProvider)
        {

        }

        public User GetUser(int id)
        {
            return _context.Users.Include(t => t.UserProfile).SingleOrDefault(t => t.Id == id);
        }

        public User GetUser(string email)
        {
            return _context.Users.Include(t => t.UserProfile).SingleOrDefault(t => t.Email == email);
        }

        public List<User> GetUsers()
        {
            return _context.Users.Include(t => t.UserProfile).ToList();
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var userProfile = _context.UserProfiles.Find(id);
            var user = _context.Users.Find(id);
            _context.UserProfiles.Remove(userProfile);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public Task<SignInResult> PasswordSignInAsync(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Task.FromResult(SignInResult.Failed);
            }
            var user = _context.Users.Include(t=>t.UserProfile)
                .FirstOrDefault(t => t.Email == email && t.Password == password);
            if(user == null)
            {
                return Task.FromResult(SignInResult.Failed);
            }

            var result = SignInResult.Success;
            result.Name = user.FullName;
            result.Email = user.Email;
            return Task.FromResult(result);
        }
    }
}
