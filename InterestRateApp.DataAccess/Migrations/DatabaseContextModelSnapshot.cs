﻿// <auto-generated />
using System;
using InterestRateApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InterestRateApp.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InterestRateApp.Domain.Entities.AgreementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("BaseRateCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<decimal>("Margin")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Agreements");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c25e824-2582-497c-8c92-0d39da9f74bf"),
                            Amount = 12000,
                            BaseRateCode = "VILIBOR1m",
                            CustomerId = new Guid("c7e7d3b9-1f25-4609-bbec-2fa1b7564561"),
                            Duration = 60,
                            Margin = 1.6m
                        },
                        new
                        {
                            Id = new Guid("a686e94a-81da-4990-8a83-caa45358896d"),
                            Amount = 8000,
                            BaseRateCode = "VILIBOR3m",
                            CustomerId = new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"),
                            Duration = 36,
                            Margin = 2.2m
                        },
                        new
                        {
                            Id = new Guid("e3f20c0f-a742-4684-ace1-da3470c4c990"),
                            Amount = 8000,
                            BaseRateCode = "VILIBOR1y",
                            CustomerId = new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"),
                            Duration = 24,
                            Margin = 1.85m
                        });
                });

            modelBuilder.Entity("InterestRateApp.Domain.Entities.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalId")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.HasIndex("PersonalId")
                        .IsUnique()
                        .HasFilter("[PersonalId] IS NOT NULL");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c7e7d3b9-1f25-4609-bbec-2fa1b7564561"),
                            FirstName = "Peter",
                            LastName = "Peterson",
                            PersonalId = "12345678901"
                        },
                        new
                        {
                            Id = new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"),
                            FirstName = "Robert",
                            LastName = "Robertson",
                            PersonalId = "01987654321"
                        });
                });

            modelBuilder.Entity("InterestRateApp.Domain.Entities.AgreementEntity", b =>
                {
                    b.HasOne("InterestRateApp.Domain.Entities.CustomerEntity", "Customer")
                        .WithMany("Agreements")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}