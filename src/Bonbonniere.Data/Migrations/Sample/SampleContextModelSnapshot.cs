using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bonbonniere.Infrastructure.Data;

namespace Bonbonniere.Data.Migrations.Sample
{
    [DbContext(typeof(SampleContext))]
    partial class SampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Album", b =>
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

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.BrainstormSession", b =>
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

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Genre", b =>
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

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Idea", b =>
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

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Root", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Example");

                    b.Property<string>("ListOfWords");

                    b.Property<string>("Meanings");

                    b.Property<string>("Origin");

                    b.Property<string>("RootWord");

                    b.HasKey("Id");

                    b.ToTable("Roots");
                });

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Suffix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Example");

                    b.Property<string>("ListOfWords");

                    b.Property<string>("Meanings");

                    b.Property<string>("SuffixWord");

                    b.HasKey("Id");

                    b.ToTable("Suffixes");
                });

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Album", b =>
                {
                    b.HasOne("Bonbonniere.Core.Sample.Model.Genre", "Genre")
                        .WithMany("Albums")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("Bonbonniere.Core.Sample.Model.Idea", b =>
                {
                    b.HasOne("Bonbonniere.Core.Sample.Model.BrainstormSession", "BrainstormSession")
                        .WithMany("Ideas")
                        .HasForeignKey("BrainstormSessionId");
                });
        }
    }
}
