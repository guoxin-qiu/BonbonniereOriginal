using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Core.EnglishClass.Model;

namespace Bonbonniere.Data.Migrations.EnglishClass
{
    [DbContext(typeof(EnglishClassContext))]
    partial class EnglishClassContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.Enrollment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamID");

                    b.Property<int>("Grade");

                    b.Property<int>("StudentID");

                    b.HasKey("ID");

                    b.HasIndex("ExamID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.Exam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Exams");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Exam");
                });

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.ScoringExam", b =>
                {
                    b.HasBaseType("Bonbonniere.Core.EnglishClass.Model.Exam");

                    b.Property<decimal>("TotalScore");

                    b.ToTable("ScoringExam");

                    b.HasDiscriminator().HasValue("ScoringExam");
                });

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.UnscoringExam", b =>
                {
                    b.HasBaseType("Bonbonniere.Core.EnglishClass.Model.Exam");

                    b.Property<string>("Evaluation");

                    b.ToTable("UnscoringExam");

                    b.HasDiscriminator().HasValue("UnscoringExam");
                });

            modelBuilder.Entity("Bonbonniere.Core.EnglishClass.Model.Enrollment", b =>
                {
                    b.HasOne("Bonbonniere.Core.EnglishClass.Model.Exam", "Exam")
                        .WithMany("Enrollments")
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bonbonniere.Core.EnglishClass.Model.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
