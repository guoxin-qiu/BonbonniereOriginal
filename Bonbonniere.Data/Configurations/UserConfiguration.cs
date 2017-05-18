using Bonbonniere.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Configurations
{
    public class UserConfiguration
    {
        public UserConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<User>();

            entityBuilder.ToTable("Users");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Username).HasMaxLength(50).IsRequired();
            entityBuilder.Property(t => t.Email).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Password).HasMaxLength(100).IsRequired();

            entityBuilder.HasOne(t => t.UserProfile).WithOne(u => u.User).HasForeignKey<UserProfile>(x => x.Id); // TODO: deeply
        }
    }
}
