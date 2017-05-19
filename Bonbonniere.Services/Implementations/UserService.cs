﻿using Bonbonniere.Core.Models;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Services.Interfaces;
using System.Collections.Generic;
using Bonbonniere.Services.ServiceModels;
using System;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Implementations
{
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

        public Task<SignInResult> PasswordSignInAsync(string username, string password, bool isPersistent)
        {
            throw new NotImplementedException(); // TODO: need to implement PasswordSignInAsync
        }
    }
}
