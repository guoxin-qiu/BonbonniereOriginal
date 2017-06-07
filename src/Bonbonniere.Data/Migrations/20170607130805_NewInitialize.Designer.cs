using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bonbonniere.Data.Migrations;
using Bonbonniere.Core.Models;

namespace Bonbonniere.Data.Migrations
{
    [DbContext(typeof(MSSQLDataContext))]
    [Migration("20170607130805_NewInitialize")]
    partial class NewInitialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bonbonniere.Core.Models.BrainstormSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("BrainstormSessions");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrainstormSessionId");

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("BrainstormSessionId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.MusicStore.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtUrl")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int?>("GenreId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.MusicStore.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.User", b =>
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

            modelBuilder.Entity("Bonbonniere.Core.Models.UserProfile", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Gender");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.Idea", b =>
                {
                    b.HasOne("Bonbonniere.Core.Models.BrainstormSession", "BrainstormSession")
                        .WithMany("Ideas")
                        .HasForeignKey("BrainstormSessionId");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.MusicStore.Album", b =>
                {
                    b.HasOne("Bonbonniere.Core.Models.MusicStore.Genre", "Genre")
                        .WithMany("Albums")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.UserProfile", b =>
                {
                    b.HasOne("Bonbonniere.Core.Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Bonbonniere.Core.Models.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
