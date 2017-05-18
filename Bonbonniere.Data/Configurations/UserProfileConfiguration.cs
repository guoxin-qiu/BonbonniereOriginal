using Bonbonniere.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Configurations
{
    public class UserProfileConfiguration
    {
        public UserProfileConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<UserProfile>();

            entityBuilder.ToTable("UserProfiles");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.LastName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
            entityBuilder.Property(t => t.Address).HasMaxLength(200).IsRequired();
            entityBuilder.Property(t => t.IPAddress).HasMaxLength(50).IsRequired();
        }
    }
}
