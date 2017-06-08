using Bonbonniere.Core.EnglishClass.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bonbonniere.Infrastructure.Data
{
    public class EnglishClassContext: DbContext
    {
        public EnglishClassContext(DbContextOptions<EnglishClassContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ScoringExam> ScoringExams { get; set; }
        public DbSet<UnscoringExam> UnscoringExams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(ConfigureStudent);
        }

        void ConfigureStudent(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.ToTable("Student");

            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.FirstName).HasMaxLength(20).IsRequired();
            entityBuilder.Property(t => t.LastName).HasMaxLength(20).IsRequired();
            entityBuilder.Property(t => t.ChineseName).HasMaxLength(20).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
        }
    }
}
