using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Bonbonniere.Website.Features.Home;
using Bonbonniere.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Bonbonniere.Website.Features.Account
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            #region only a logging test
            _logger.LogDebug("LogDebug: Register!");
            _logger.LogInformation("LogInformation: Register!");
            _logger.LogTrace("LogTrace: Register!");
            _logger.LogWarning("LogWarning: Register!");
            _logger.LogError("LogError: Register!");
            _logger.LogCritical("LogCritical: Register!");
            #endregion only a logging test

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
                _userService.InsertUser(user);
                return RedirectToAction("Registration", new { id = user.Id, isNew = true });
            }
            return View(model);
        }

        public IActionResult Registration(int id, bool isNew = false)
        {
            var user = _userService.GetUser(id);
            if (user == null)
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string returnUrl = null)
        {
            //string _externalCookieScheme = string.Empty; // TODO: empty cookie scheme
            //await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _userService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
