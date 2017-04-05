using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Bonbonniere.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegisterRepository _registerRepository;

        public AccountController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
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
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                _registerRepository.Save(model);
                return RedirectToAction("Registration", new { id = model.Id, isNew = true });
            }
            return View(model);
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