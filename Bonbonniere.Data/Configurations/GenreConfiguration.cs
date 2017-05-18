using Microsoft.EntityFrameworkCore;
using Bonbonniere.Core.Models.MusicStore;

namespace Bonbonniere.Data.Configurations
{
    public class GenreConfiguration
    {
        public GenreConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Genre>();

            entityBuilder.ToTable("Genres");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();

            entityBuilder.HasMany(t => t.Albums).WithOne(t => t.Genre);
        }
    }
}
