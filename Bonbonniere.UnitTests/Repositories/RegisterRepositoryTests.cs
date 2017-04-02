using Bonbonniere.Core.Models;
using Bonbonniere.Infrastructure.Repositories;
using Xunit;

namespace Bonbonniere.UnitTests.Repositories
{
    public class RegisterRepositoryTests
    {
        [Fact]
        public void Register_new_account_should_assign_an_id()
        {
            var register = new Register
            {
                Username = "Repository Test",
                Email = "Repository Test",
                Password = "Password",
                ConfirmPassword = "Password"
            };

            var repository = new RegisterRepository();
            repository.Save(register);

            Assert.True(register.Id > 0);
        }

        [Fact]
        public void Get_existing_registration_by_id()
        {
            var register = new Register
            {
                Username = "Repository 1",
                Email = "Repository Test 1",
                Password = "Password",
                ConfirmPassword = "Password"
            };

            var repository = new RegisterRepository();
            repository.Save(register);

            Register fechedRegister = repository.Get(register.Id);

            Assert.NotNull(fechedRegister);
            Assert.Equal(register.Id, fechedRegister.Id);
            Assert.Equal(register.Username, fechedRegister.Username);
        } 
    }
}
