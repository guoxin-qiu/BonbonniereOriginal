using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bonbonniere.Infrastructure.Data
{
    public class EnglishClassContextInitializer
    {
        public static void Initialize(EnglishClassContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Students.Any())
            {

                context.SaveChanges();
            }
        }
    }
}
