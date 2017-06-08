using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Core.App.Model;

namespace Bonbonniere.Data.Migrations.Apps
{
    [DbContext(typeof(AppsContext))]
    partial class AppsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bonbonniere.Core.App.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bonbonniere.Core.App.Model.UserProfile", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Gender");

                    b.Property<string>("IPAddress")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Bonbonniere.Core.App.Model.UserProfile", b =>
                {
                    b.HasOne("Bonbonniere.Core.App.Model.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Bonbonniere.Core.App.Model.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
