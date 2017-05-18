using Microsoft.EntityFrameworkCore;
using Bonbonniere.Core.Models;

namespace Bonbonniere.Data.Configurations
{
    public class BrainstormSessionConfiguration
    {
        public BrainstormSessionConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<BrainstormSession>();

            entityBuilder.ToTable("BrainstormSessions");

            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            entityBuilder.HasMany(t => t.Ideas).WithOne(t => t.BrainstormSession);
        }
    }
}