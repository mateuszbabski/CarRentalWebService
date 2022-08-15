﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("FullPriceForRenting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int?>("RentalCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RentalCompanyId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Domain.Entities.RentalCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RentalCompanies");
                });

            modelBuilder.Entity("Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("DailyCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RentalCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservationStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RentalCompanyId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<decimal>("DailyCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FuelType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<int?>("RentalCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RentalCompanyId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("InvoicesHistory")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Domain.Entities.RentalCompany", "RentalCompany")
                        .WithMany("InvoicesHistory")
                        .HasForeignKey("RentalCompanyId");

                    b.HasOne("Domain.Entities.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId");

                    b.HasOne("Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Customer");

                    b.Navigation("RentalCompany");

                    b.Navigation("Reservation");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Domain.Entities.Reservation", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "Customer")
                        .WithMany("ReservationsHistory")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Domain.Entities.RentalCompany", "RentalCompany")
                        .WithMany("ReservationList")
                        .HasForeignKey("RentalCompanyId");

                    b.HasOne("Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Customer");

                    b.Navigation("RentalCompany");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Domain.Entities.Vehicle", b =>
                {
                    b.HasOne("Domain.Entities.RentalCompany", "RentalCompany")
                        .WithMany("VehicleList")
                        .HasForeignKey("RentalCompanyId");

                    b.Navigation("RentalCompany");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Navigation("InvoicesHistory");

                    b.Navigation("ReservationsHistory");
                });

            modelBuilder.Entity("Domain.Entities.RentalCompany", b =>
                {
                    b.Navigation("InvoicesHistory");

                    b.Navigation("ReservationList");

                    b.Navigation("VehicleList");
                });
#pragma warning restore 612, 618
        }
    }
}
