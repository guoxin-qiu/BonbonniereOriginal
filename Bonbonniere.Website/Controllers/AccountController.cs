using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Website.ViewModels;

namespace Bonbonniere.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;

        public AccountController(IRepository<User> userRepository,IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Username = model.Username, Email = model.Email, Password = model.Password };
                _userRepository.Save(user);
                _uow.Commit();
                return RedirectToAction("Registration", new { id = user.Id, isNew = true });
            }
            return View(model);
        }

        public IActionResult Registration(int id, bool isNew = false)
        {
            var user = _userRepository.GetById(id);
            if(user == null)
            {
                return BadRequest();
            }

            if (isNew)
            {
                ViewBag.Message = "Account Created!";
            }
            var model = new RegisterViewModel { Username = user.Username, Email = user.Email, Password = user.Password };
            return View("Registration", model);
        }
    }
}