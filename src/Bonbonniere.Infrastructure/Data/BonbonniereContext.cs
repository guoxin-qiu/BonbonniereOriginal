using Bonbonniere.Core.Models;
using Bonbonniere.Core.Models.MusicStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bonbonniere.Infrastructure.Data
{
    public class BonbonniereContext : DbContext
    {
        public BonbonniereContext()
        {
        }

        public BonbonniereContext(DbContextOptions options) 
            : base(options)
        {
        }

        #region Db Set
        public DbSet<Album> Albums { get; set; }
        public DbSet<BrainstormSession> BrainstormSessions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        #endregion Db Set

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>(ConfigureAlbum);
            modelBuilder.Entity<BrainstormSession>(ConfigureBrainstormSession);
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Idea>(ConfigureIdea);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<UserProfile>(ConfigureUserProfile);
        }

        #region Model Configuration
        void ConfigureAlbum(EntityTypeBuilder<Album> entityBuilder)
        {
            entityBuilder.ToTable("Albums");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ArtUrl).HasMaxLength(200).IsRequired();
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.Title).HasMaxLength(160).IsRequired();
            entityBuilder.Property(t => t.Price).IsRequired();
        }
        void ConfigureBrainstormSession(EntityTypeBuilder<BrainstormSession> entityBuilder)
        {
            entityBuilder.ToTable("BrainstormSessions");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            entityBuilder.HasMany(t => t.Ideas).WithOne(t => t.BrainstormSession);
        }
        void ConfigureGenre(EntityTypeBuilder<Genre> entityBuilder)
        {
            entityBuilder.ToTable("Genres");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();

            entityBuilder.HasMany(t => t.Albums).WithOne(t => t.Genre);
        }
        void ConfigureIdea(EntityTypeBuilder<Idea> entityBuilder)
        {
            entityBuilder.ToTable("Ideas");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.DateCreated).IsRequired();
            entityBuilder.Property(t => t.Description).HasMaxLength(500).IsRequired();
        }
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
            entityBuilder.Property(t => t.Address).HasMaxLength(200).IsRequired();
            entityBuilder.Property(t => t.IPAddress).HasMaxLength(50).IsRequired();
        }
        #endregion Model Configuration
    }
}
