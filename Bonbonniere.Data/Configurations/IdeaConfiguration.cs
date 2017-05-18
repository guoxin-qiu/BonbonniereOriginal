using Microsoft.EntityFrameworkCore;
using Bonbonniere.Core.Models;

namespace Bonbonniere.Data.Configurations
{
    public class IdeaConfiguration
    {
        public IdeaConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Idea>();

            entityBuilder.ToTable("Ideas");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.DateCreated).IsRequired();
            entityBuilder.Property(t => t.Description).HasMaxLength(500).IsRequired();
        }
    }
}