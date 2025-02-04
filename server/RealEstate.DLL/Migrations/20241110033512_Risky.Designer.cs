﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.DLL.EF;

#nullable disable

namespace RealEstate.DLL.Migrations
{
    [DbContext(typeof(RealEstateContext))]
    [Migration("20241110033512_Risky")]
    partial class Risky
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstate.DLL.Entites.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CheckIn")
                        .HasColumnType("int");

                    b.Property<int?>("CheckOut")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtraInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<string>("Perks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.BookingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppartmentId")
                        .HasColumnType("int");

                    b.Property<string>("CheckIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CheckOut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AppartmentId");

                    b.HasIndex("ClientId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.Apartment", b =>
                {
                    b.HasOne("RealEstate.DLL.Entites.Client", "Client")
                        .WithMany("Apartments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.BookingModel", b =>
                {
                    b.HasOne("RealEstate.DLL.Entites.Apartment", "Appartment")
                        .WithMany("Bookings")
                        .HasForeignKey("AppartmentId");

                    b.HasOne("RealEstate.DLL.Entites.Client", "Client")
                        .WithMany("Bookings")
                        .HasForeignKey("ClientId");

                    b.Navigation("Appartment");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.Apartment", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("RealEstate.DLL.Entites.Client", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
