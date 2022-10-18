﻿// <auto-generated />
using System;
using Holidays_WebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Holidays_WebAPI.Migrations
{
    [DbContext(typeof(HolidayDbContext))]
    partial class HolidayDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Holiday", b =>
                {
                    b.Property<int>("HolidayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HolidayType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("HolidayId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Name", b =>
                {
                    b.Property<int>("NameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("HolidayId")
                        .HasColumnType("int");

                    b.Property<string>("Lang")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("NameId");

                    b.HasIndex("CountryId");

                    b.HasIndex("HolidayId");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RegionId");

                    b.HasIndex("CountryId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Name", b =>
                {
                    b.HasOne("Holidays_WebAPI.Models.DbModels.Country", null)
                        .WithMany("Names")
                        .HasForeignKey("CountryId");

                    b.HasOne("Holidays_WebAPI.Models.DbModels.Holiday", null)
                        .WithMany("Names")
                        .HasForeignKey("HolidayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Region", b =>
                {
                    b.HasOne("Holidays_WebAPI.Models.DbModels.Country", null)
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Country", b =>
                {
                    b.Navigation("Names");

                    b.Navigation("Regions");
                });

            modelBuilder.Entity("Holidays_WebAPI.Models.DbModels.Holiday", b =>
                {
                    b.Navigation("Names");
                });
#pragma warning restore 612, 618
        }
    }
}