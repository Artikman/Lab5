﻿// <auto-generated />
using System;
using Lab_4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab_4.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20190408190749_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab_4.Models.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgeRestrictions");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Duration");

                    b.Property<string>("FilmCompany");

                    b.Property<string>("Genre");

                    b.Property<int>("GenreId");

                    b.Property<string>("ListOfMainActros");

                    b.Property<string>("Name");

                    b.Property<string>("ProducingCountry");

                    b.Property<int>("SessionId");

                    b.HasKey("FilmId");

                    b.HasIndex("GenreId");

                    b.HasIndex("SessionId");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Lab_4.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Lab_4.Models.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Employment");

                    b.Property<int>("PlaceNumber");

                    b.Property<string>("Session");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("Lab_4.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmployessInvolvedInTheSession");

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("PlaceId");

                    b.Property<float>("TicketPrice");

                    b.Property<DateTime>("TimeStarted");

                    b.HasKey("SessionId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Lab_4.Models.Film", b =>
                {
                    b.HasOne("Lab_4.Models.Genre", "Genres")
                        .WithMany("Films")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab_4.Models.Session", "Sessions")
                        .WithMany("Films")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lab_4.Models.Session", b =>
                {
                    b.HasOne("Lab_4.Models.Place", "Places")
                        .WithMany("Sessions")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}