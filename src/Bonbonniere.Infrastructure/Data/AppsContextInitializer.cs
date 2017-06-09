using Bonbonniere.Core.App.Model;
using System.Linq;

namespace Bonbonniere.Infrastructure.Data
{
    public static class AppsContextInitializer
    {
        public static void Initialize(AppsContext context)
        {
            context.Database.EnsureCreated();

            #region Initialize User
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User
                    {
                        Username = "Administrator",
                        Email = "admin@admin.net",
                        Password = "123456",
                        UserProfile = new UserProfile
                        {
                            FirstName = "Denis",
                            LastName = "Qiu",
                            Gender = Gender.Male,
                            IPAddress = "10.10.20.154",
                            Address = "Dalian"
                        }
                    }
                };
                context.AddRange(users);
                context.SaveChanges();
            }
            #endregion Initialize User
        }
    }
}