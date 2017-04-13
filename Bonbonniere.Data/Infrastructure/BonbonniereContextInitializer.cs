using Bonbonniere.Core.Models;
using Bonbonniere.Core.Models.MusicStore;
using System.Linq;

namespace Bonbonniere.Data.Infrastructure
{
    public static class BonbonniereContextInitializer
    {
        public static void Initialize(BonbonniereContext context)
        {
            context.Database.EnsureCreated();

            #region Initialize User
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User { Username = "Administrator", Email = "admin@admin.net", Password = "123456" }
                };
                
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }

                context.SaveChanges();
            }
            #endregion Initialize User

            #region Initialize Music Store
            if (!context.Albums.Any())
            {
                var albums = new Album[]
                {
                    new Album{ Title = "First Album" },
                    new Album{ Title = "Second Album" }
                };

                context.Albums.AddRange(albums);
                context.SaveChanges();
            }
            #endregion Initialize Music Store
        }
    }
}