﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Space.MovieSearcher.Infrastructure;

#nullable disable

namespace Space.MovieSearcher.Infrastructure.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    partial class MoviesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Space.MovieSearcher.Domain.Watchlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Watchlist");
                });

            modelBuilder.Entity("Space.MovieSearcher.Domain.WatchlistMovie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsMovieWatched")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastOfferDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MovieId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WatchlistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WatchlistId");

                    b.ToTable("WatchlistMovie");
                });

            modelBuilder.Entity("Space.MovieSearcher.Domain.WatchlistMovie", b =>
                {
                    b.HasOne("Space.MovieSearcher.Domain.Watchlist", "Watchlist")
                        .WithMany()
                        .HasForeignKey("WatchlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Watchlist");
                });
#pragma warning restore 612, 618
        }
    }
}
