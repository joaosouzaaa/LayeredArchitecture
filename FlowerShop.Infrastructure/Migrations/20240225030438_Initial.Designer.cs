﻿// <auto-generated />
using System;
using FlowerShop.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlowerShop.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240225030438_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlowerShop.Domain.Entites.Flower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BloomingSeason")
                        .HasColumnType("int")
                        .HasColumnName("blooming_season");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("color");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("species");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Flowers", "FlowerShop");
                });

            modelBuilder.Entity("FlowerShop.Domain.Entites.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnName("email");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Shops", "FlowerShop");
                });

            modelBuilder.Entity("FlowerShop.Domain.Entites.Flower", b =>
                {
                    b.HasOne("FlowerShop.Domain.Entites.Shop", "Shop")
                        .WithMany("Flowers")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Flower_Shop");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("FlowerShop.Domain.Entites.Shop", b =>
                {
                    b.Navigation("Flowers");
                });
#pragma warning restore 612, 618
        }
    }
}
