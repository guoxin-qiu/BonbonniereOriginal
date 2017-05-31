using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Bonbonniere.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using System;

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
        public IActionResult Register(string returnUrl = null)
        {
            #region only a logging test
            _logger.LogDebug("LogDebug: Register!");
            _logger.LogInformation("LogInformation: Register!");
            _logger.LogTrace("LogTrace: Register!");
            _logger.LogWarning("LogWarning: Register!");
            _logger.LogError("LogError: Register!");
            _logger.LogCritical("LogCritical: Register!");
            #endregion only a logging test

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    UserProfile = new UserProfile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Gender = (Gender)model.Gender
                    }
                };
                _userService.InsertUser(user);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.GivenName, user.FullName, ClaimValueTypes.String, null),
                        new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String, null)
                    };
                var userIdentity = new ClaimsIdentity("SuperSecureLogin");
                userIdentity.AddClaims(claims);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });

                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        public IActionResult MyRegistration()
        {
            var signInEmail = HttpContext.User.Identity.Name;
            var user = _userService.GetUser(signInEmail);
            if (user == null)
            {
                return BadRequest();
            }

            var model = new RegisterViewModel
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                Gender = (int)user.UserProfile.Gender
            };
            return View("Registration", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null)
        {
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
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.Email, ClaimValueTypes.String, null),
                        new Claim(ClaimTypes.Name, result.Name, ClaimValueTypes.String, null)
                    };
                    var userIdentity = new ClaimsIdentity("SuperSecureLogin");
                    userIdentity.AddClaims(claims);
                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                            IsPersistent = false,
                            AllowRefresh = false
                        });

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToLocal("/");
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
