using Bonbonniere.Core.Models;
using Bonbonniere.Infrastructure.Domain;
using System.Collections.Generic;

namespace Bonbonniere.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IUnitOfWork _uow;

        public UserService(
            IRepository<User> userRepository, 
            IRepository<UserProfile> userProfileRepository,
            IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _uow = uow;
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(t => t.Id == id, r => r.UserProfile);
        }

        public List<User> GetUsers()
        {
            return _userRepository.FetchAll(t=>t.UserProfile);
        }

        public void InsertUser(User user)
        {
            _userRepository.Add(user);
            _uow.Commit();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _uow.Commit();
        }

        public void DeleteUser(int id)
        {
            _userProfileRepository.Remove(id);
            _userRepository.Remove(id);
            _uow.Commit();
        }
    }
}
