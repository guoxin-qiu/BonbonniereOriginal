using Bonbonniere.Core.Models;
using System.Linq;

namespace Bonbonniere.Data.Infrastructure
{
    public static class BonbonniereContextInitializer
    {
        public static void Initialize(IDataProvider dataProvider)
        {
            var context = dataProvider.DbContext;

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; //DB has been seeded
            }

            var users = new User[]
            {
                new User{ Username = "Administrator", Email="admin@admin.net",Password="123456"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
