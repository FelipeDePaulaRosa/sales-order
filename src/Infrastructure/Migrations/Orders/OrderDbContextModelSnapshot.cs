﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations.Orders
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Order", "dbo");
                });

            modelBuilder.Entity("Domain.Orders.OrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct", "dbo");
                });

            modelBuilder.Entity("Domain.Orders.OrderStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderStatusHistory", "dbo");
                });

            modelBuilder.Entity("Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product", "dbo");
                });

            modelBuilder.Entity("Domain.Orders.Order", b =>
                {
                    b.OwnsOne("Domain.Orders.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("ValueInCents")
                                .HasColumnType("bigint")
                                .HasColumnName("Amount");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Domain.Orders.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Status")
                                .HasColumnType("int")
                                .HasColumnName("Status");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Orders.OrderProduct", b =>
                {
                    b.HasOne("Domain.Orders.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Orders.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid>("OrderProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("ValueInCents")
                                .HasColumnType("bigint")
                                .HasColumnName("Amount");

                            b1.HasKey("OrderProductId");

                            b1.ToTable("OrderProduct", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderProductId");
                        });

                    b.OwnsOne("Domain.Shared.ValueObjects.Discount", "Discount", b1 =>
                        {
                            b1.Property<Guid>("OrderProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("ValueInPercentage")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Discount");

                            b1.HasKey("OrderProductId");

                            b1.ToTable("OrderProduct", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderProductId");
                        });

                    b.OwnsOne("Domain.Orders.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("ValueInCents")
                                .HasColumnType("bigint")
                                .HasColumnName("UnitPrice");

                            b1.HasKey("OrderProductId");

                            b1.ToTable("OrderProduct", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderProductId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("Discount")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Orders.OrderStatusHistory", b =>
                {
                    b.HasOne("Domain.Orders.Order", null)
                        .WithMany("StatusHistory")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Orders.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("OrderStatusHistoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Status")
                                .HasColumnType("int")
                                .HasColumnName("Status");

                            b1.HasKey("OrderStatusHistoryId");

                            b1.ToTable("OrderStatusHistory", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderStatusHistoryId");
                        });

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Products.Product", b =>
                {
                    b.OwnsOne("Domain.Shared.ValueObjects.Discount", "Discount", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("ValueInPercentage")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Discount");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Domain.Orders.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("ValueInCents")
                                .HasColumnType("bigint")
                                .HasColumnName("UnitPrice");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Discount")
                        .IsRequired();

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Orders.Order", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("StatusHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
