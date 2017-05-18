using Microsoft.EntityFrameworkCore;
using Bonbonniere.Core.Models.MusicStore;

namespace Bonbonniere.Data.Configurations
{
    public class AlbumConfiguration
    {
        public AlbumConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Album>();

            entityBuilder.ToTable("Albums");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ArtUrl).HasMaxLength(200).IsRequired();
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.Title).HasMaxLength(160).IsRequired();
            entityBuilder.Property(t => t.Price).IsRequired();
        }
    }
}
