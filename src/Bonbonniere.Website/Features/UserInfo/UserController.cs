using Bonbonniere.Core.Models;
using Bonbonniere.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bonbonniere.Website.Features.UserInfo
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _userService.GetUsers()
                .Select(t => new UserViewModel
                {
                    Id = t.Id,
                    Email = t.Email,
                    Name = t.FullName,
                    Address = t.UserProfile.Address
                }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var model = new UserViewModel();

            return PartialView("_AddUser", model);
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel model)
        {
            if (model.Password.Length < 6)
            {
                return PartialView("_AddUser", model);
            }

            //TODO: Use DTO
            var userEntity = new User
            {
                Username = model.UserName,
                Email = model.Email,
                Password = model.Password,
                UserProfile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Address = model.Address,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                }
            };

            _userService.InsertUser(userEntity);
            if(userEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_AddUser", model);
        }

        [HttpGet]
        public IActionResult EditUser(int? id)
        {
            var model = new UserViewModel();
            if(id.HasValue && id != 0)
            {
                var userEntity = _userService.GetUser(id.Value);
                model = new UserViewModel
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.UserProfile.FirstName,
                    LastName = userEntity.UserProfile.LastName,
                    Address = userEntity.UserProfile.Address,
                    Email = userEntity.Email
                };
            }

            return PartialView("_EditUser", model);
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userEntity = _userService.GetUser(model.Id);
            userEntity.Email = model.Email;
            userEntity.UserProfile.FirstName = model.FirstName;
            userEntity.UserProfile.LastName = model.LastName;
            userEntity.UserProfile.Address = model.Address;
            userEntity.UserProfile.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _userService.UpdateUser(userEntity);

            if (userEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_EditUser", model);
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var userEntity = _userService.GetUser(id);
            string name = userEntity.FullName;
            return PartialView("_DeleteUser", name);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id, IFormCollection form)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
