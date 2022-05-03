﻿// <auto-generated />
using System;
using Authn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Authn.Data
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20220502224422_RepairTimeFiledDeleted")]
    partial class RepairTimeFiledDeleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Authn.Data.AppUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("NameIdentifier")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "Rzebika 1/3",
                            City = "Krakow",
                            Country = "Poland",
                            Email = "bob@donet.pl",
                            FirstName = "Bob",
                            LastName = "Tester",
                            Mobile = "333 333 333",
                            NameIdentifier = "bob",
                            Password = "pizza",
                            Provider = "Cookies",
                            Roles = "Admin",
                            UserName = "bob"
                        });
                });

            modelBuilder.Entity("Authn.Models.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Delivery")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Repair");
                });
#pragma warning restore 612, 618
        }
    }
}
