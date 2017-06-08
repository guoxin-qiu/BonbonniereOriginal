using Bonbonniere.Core.Sample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bonbonniere.Infrastructure.Data
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
            
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<BrainstormSession> BrainstormSessions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Root> Roots { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>(ConfigureAlbum);
            modelBuilder.Entity<BrainstormSession>(ConfigureBrainstormSession);
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Idea>(ConfigureIdea);
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
        #endregion Model Configuration
    }
}
