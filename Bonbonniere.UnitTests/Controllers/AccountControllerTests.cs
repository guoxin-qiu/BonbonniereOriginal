using Bonbonniere.Core.Models;
using Bonbonniere.Infrastructure.Repositories;
using Bonbonniere.Website.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Bonbonniere.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public void Register_should_return_register_view_with_empty_register_object()
        {
            var controller = new AccountController(new RegisterRepository());
            var result = controller.Register() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Register", result.ViewName);

            var model = result.ViewData.Model;
            Assert.IsType(typeof(Register), model);
        }

        [Fact]
        public void Register_with_valid_info_should_create_account_and_rediret_to_register_detail_view()
        {
            var model = new Register
            {
                Username = "Denis",
                Email = "denis@qyq.net",
                Password = "p@55w0rd!",
                ConfirmPassword = "p@55w0rd!"
            };

            var controller = new AccountController(new RegisterRepository());
            controller.ValidateModel(model);
            var result = controller.Register(model);

            Assert.True(model.Id > 0);
            Assert.NotNull(result);
            Assert.IsType(typeof(RedirectToActionResult), result);
            var actionResult = result as RedirectToActionResult;

            Assert.Equal("Registration", actionResult.ActionName);
        }

        [Theory]
        [InlineData("", "dennis@qyq.net", "p@55w0rd!", "p@55w0rd!", "Username is required.")]
        [InlineData("", "", "p@55w0rd!", "p@55w0rd!", "Username is required.Email is required. ")]
        [InlineData("Dennis", "dennis@", "p@55w0rd!", "p@55w0rd!", "Email is not valid.  ")]
        [InlineData("Dennis", "dennis@qyq.net", "p@55", "p@55", "Password must be longer than 5 characters.")]
        //[InlineData("Dennis", "dennis@qyq.net", "p@55w0rd!", "p@55w0rd", "Password not match. ")]
        public void Register_with_invalid_info_should_display_error_message(string username, string email, string password, string confirmPassword, string errorMsg)
        {
            var registerModel = new Register
            {
                Username = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var controller = new AccountController(new RegisterRepository());
            controller.ValidateModel(registerModel);
            var result = controller.Register(registerModel);

            Assert.NotNull(result);
            Assert.IsType(typeof(ViewResult), result);

            var viewResult = result as ViewResult;
            Assert.Equal("Register", viewResult.ViewName);
            Assert.Equal(registerModel, viewResult.ViewData.Model);
        }
    }
}
