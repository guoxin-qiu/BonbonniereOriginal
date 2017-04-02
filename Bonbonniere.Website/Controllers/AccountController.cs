using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bonbonniere.Website.Controllers
{
    public class AccountController : Controller
    {
        private IRegisterRepository _registerRepository;

        public AccountController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register", new Register());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                _registerRepository.Save(model);
                return RedirectToAction("Registration", new { id = model.Id, isNew = true });
            }
            return View("Register", model);
        }

        public IActionResult Registration(int id, bool isNew = false)
        {
            var model = _registerRepository.Get(id);
            if (isNew)
            {
                ViewBag.Message = "Account Created!";
            }
            return View("Registration", model);
        }
    }
}