using Bonbonniere.Core.App.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bonbonniere.Infrastructure.Data
{
    public class AppsContext : DbContext
    {
        public AppsContext(DbContextOptions<AppsContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<UserProfile>(ConfigureUserProfile);
        }

        #region Model Configuration
        void ConfigureUser(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.ToTable("Users");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Username).HasMaxLength(50).IsRequired();
            entityBuilder.Property(t => t.Email).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Password).HasMaxLength(100).IsRequired();

            entityBuilder.HasOne(t => t.UserProfile).WithOne(u => u.User).HasForeignKey<UserProfile>(x => x.Id); // TODO: deeply
        }
        void ConfigureUserProfile(EntityTypeBuilder<UserProfile> entityBuilder)
        {
            entityBuilder.ToTable("UserProfiles");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.LastName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
            entityBuilder.Property(t => t.Address).HasMaxLength(200);
            entityBuilder.Property(t => t.IPAddress).HasMaxLength(50);
        }
        #endregion Model Configuration
    }
}
