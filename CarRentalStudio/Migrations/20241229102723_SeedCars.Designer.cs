﻿// <auto-generated />
using System;
using CarRentalStudio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalStudio.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241229102723_SeedCars")]
    partial class SeedCars
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRentalStudio.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Acceleration")
                        .HasColumnType("float");

                    b.Property<int>("BodyType")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DailyRate")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Drive")
                        .HasColumnType("int");

                    b.Property<float>("EngineCapacity")
                        .HasColumnType("real");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<int>("HorsePower")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Mileage")
                        .HasColumnType("real");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Torque")
                        .HasColumnType("int");

                    b.Property<int>("Transmission")
                        .HasColumnType("int");

                    b.Property<double>("VMax")
                        .HasColumnType("float");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Acceleration = 2.8999999999999999,
                            BodyType = 4,
                            Brand = "Ferrari",
                            DailyRate = 1500m,
                            Drive = 1,
                            EngineCapacity = 3f,
                            FuelType = 0,
                            HorsePower = 830,
                            Image = "https://cylindersi.pl/wp-content/uploads/2023/08/Ferrari-296-GTB-Sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "296 GTB",
                            Torque = 740,
                            Transmission = 1,
                            VMax = 330.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 2,
                            Acceleration = 2.8999999999999999,
                            BodyType = 4,
                            Brand = "Ferrari",
                            DailyRate = 1500m,
                            Drive = 1,
                            EngineCapacity = 6f,
                            FuelType = 0,
                            HorsePower = 800,
                            Image = "https://cylindersi.pl/wp-content/uploads/2024/04/Ferrari-812-SUPERFAST-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "812 Superfast",
                            Torque = 718,
                            Transmission = 1,
                            VMax = 340.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 3,
                            Acceleration = 4.5999999999999996,
                            BodyType = 1,
                            Brand = "Land Rover Range Rover",
                            DailyRate = 1165m,
                            Drive = 2,
                            EngineCapacity = 4f,
                            FuelType = 1,
                            HorsePower = 530,
                            Image = "https://cylindersi.pl/wp-content/uploads/2024/11/Land-Rover-Range-Rover-SV-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "L460",
                            Torque = 750,
                            Transmission = 1,
                            VMax = 260.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 4,
                            Acceleration = 3.2999999999999998,
                            BodyType = 4,
                            Brand = "Porsche",
                            DailyRate = 1130m,
                            Drive = 2,
                            EngineCapacity = 3f,
                            FuelType = 0,
                            HorsePower = 480,
                            Image = "https://cylindersi.pl/wp-content/uploads/2022/05/911-Carrera-4-GTS-5-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "911 Carrera 4 GTS (992)",
                            Torque = 570,
                            Transmission = 1,
                            VMax = 311.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 5,
                            Acceleration = 3.1000000000000001,
                            BodyType = 4,
                            Brand = "Audi",
                            DailyRate = 1130m,
                            Drive = 2,
                            EngineCapacity = 5f,
                            FuelType = 0,
                            HorsePower = 620,
                            Image = "https://cylindersi.pl/wp-content/uploads/2023/10/Audi-R8-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "R8 Coupe V10 Performance Quattro",
                            Torque = 580,
                            Transmission = 1,
                            VMax = 331.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 6,
                            Acceleration = 4.5,
                            BodyType = 1,
                            Brand = "Mercedes-AMG",
                            DailyRate = 965m,
                            Drive = 2,
                            EngineCapacity = 4f,
                            FuelType = 0,
                            HorsePower = 585,
                            Image = "https://cylindersi.pl/wp-content/uploads/2022/10/Mercedes-AMG-G63-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "G63",
                            Torque = 850,
                            Transmission = 1,
                            VMax = 220.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 7,
                            Acceleration = 4.2999999999999998,
                            BodyType = 1,
                            Brand = "BMW",
                            DailyRate = 930m,
                            Drive = 2,
                            EngineCapacity = 4f,
                            FuelType = 4,
                            HorsePower = 653,
                            Image = "https://cylindersi.pl/wp-content/uploads/2023/10/XM-5-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "XM",
                            Torque = 800,
                            Transmission = 1,
                            VMax = 250.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 8,
                            Acceleration = 3.6000000000000001,
                            BodyType = 3,
                            Brand = "Audi",
                            DailyRate = 685m,
                            Drive = 2,
                            EngineCapacity = 4f,
                            FuelType = 0,
                            HorsePower = 630,
                            Image = "https://cylindersi.pl/wp-content/uploads/2024/07/AUDI-RS6-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "RS6",
                            Torque = 850,
                            Transmission = 1,
                            VMax = 305.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 9,
                            Acceleration = 5.4000000000000004,
                            BodyType = 0,
                            Brand = "Mercedes-Benz",
                            DailyRate = 650m,
                            Drive = 2,
                            EngineCapacity = 3f,
                            FuelType = 1,
                            HorsePower = 330,
                            Image = "https://cylindersi.pl/wp-content/uploads/2023/04/S-Klasa-5-sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "S400d Long",
                            Torque = 700,
                            Transmission = 1,
                            VMax = 250.0,
                            Year = 2024
                        },
                        new
                        {
                            Id = 10,
                            Acceleration = 5.0,
                            BodyType = 0,
                            Brand = "BMW",
                            DailyRate = 615m,
                            Drive = 2,
                            EngineCapacity = 3f,
                            FuelType = 1,
                            HorsePower = 340,
                            Image = "https://cylindersi.pl/wp-content/uploads/2023/05/BMW-740D-xDrive-Sylwetka.jpg",
                            Mileage = 1000f,
                            Model = "G70 740d xDrive",
                            Torque = 700,
                            Transmission = 1,
                            VMax = 250.0,
                            Year = 2024
                        });
                });

            modelBuilder.Entity("CarRentalStudio.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CarRentalStudio.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("CarRentalStudio.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RentalEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RentalStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CarRentalStudio.Models.Notification", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRentalStudio.Models.Rating", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarRentalStudio.Models.Rental", b =>
                {
                    b.HasOne("CarRentalStudio.Models.Car", "Car")
                        .WithMany("Rentals")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRentalStudio.Models.Car", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
