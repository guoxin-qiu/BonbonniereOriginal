using Bonbonniere.Core.Models;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Data.Repositories;
using Bonbonniere.Infrastructure;
using Bonbonniere.Website.Controllers;
using Bonbonniere.Website.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Xunit;

namespace Bonbonniere.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        AccountController _controller;

        public AccountControllerTests()
        {
            IOptions<Settings> settings = Options.Create(new Settings { DefaultConnection = "Server=(localdb)\\ProjectsV13;Database=Bonbonniere;Trusted_Connection=True;",DataProvider= DataProviderType.InMemory });
            IDataProvider _dataProvider = new DataProviderFactory(settings);
            _controller = new AccountController(
                new BaseRepository<User>(_dataProvider),
                new UnitOfWork(_dataProvider));
        }

        [Fact]
        public void Register_should_return_register_view_with_empty_register_object()
        {
            var result = _controller.Register() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Register_with_valid_info_should_create_account_and_rediret_to_register_detail_view()
        {
            var model = new RegisterViewModel
            {
                Username = "Denis",
                Email = "denis@qyq.net",
                Password = "p@55w0rd!",
                ConfirmPassword = "p@55w0rd!"
            };
            _controller.ValidateModel(model);
            var result = _controller.Register(model);

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
            var registerModel = new RegisterViewModel
            {
                Username = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            _controller.ValidateModel(registerModel);
            var result = _controller.Register(registerModel);

            Assert.NotNull(result);
            Assert.IsType(typeof(ViewResult), result);

            var viewResult = result as ViewResult;
            Assert.Equal(registerModel, viewResult.ViewData.Model);
        }
    }
}
