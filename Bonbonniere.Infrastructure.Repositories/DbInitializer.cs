using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bonbonniere.Core.Models;

namespace Bonbonniere.Infrastructure.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(BonbonniereContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; //DB has been seeded
            }

            var users = new User[] 
            {
                new User{ Username = "Administrator", Email="admin@admin.net",Password="123456"}
            };

            foreach(User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
