﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TourPlanner.Data;

#nullable disable

namespace TourPlanner.Data.Migrations
{
    [DbContext(typeof(ToursDataContext))]
    partial class ToursDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TourPlanner.Data.Adresses", b =>
                {
                    b.Property<Guid>("AdressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("Plz")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<Guid>("TourIdStart")
                        .HasColumnType("uuid");

                    b.HasKey("AdressId");

                    b.ToTable("Adress");
                });

            modelBuilder.Entity("TourPlanner.Data.Logs", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<short>("Difficulty")
                        .HasColumnType("smallint");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<short>("Rating")
                        .HasColumnType("smallint");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uuid");

                    b.HasKey("LogId");

                    b.HasIndex("TourId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("TourPlanner.Data.Tours", b =>
                {
                    b.Property<Guid>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TourIdDest")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TourIdStart")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TourId");

                    b.HasIndex("TourIdDest");

                    b.HasIndex("TourIdStart");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourPlanner.Data.Logs", b =>
                {
                    b.HasOne("TourPlanner.Data.Tours", null)
                        .WithMany("Logs")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourPlanner.Data.Tours", b =>
                {
                    b.HasOne("TourPlanner.Data.Adresses", "Destination")
                        .WithMany()
                        .HasForeignKey("TourIdDest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourPlanner.Data.Adresses", "Start")
                        .WithMany()
                        .HasForeignKey("TourIdStart")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Start");
                });

            modelBuilder.Entity("TourPlanner.Data.Tours", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
