using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElecStore.Models
{
    public partial class ElectricStore1Context : DbContext
    {
        public ElectricStore1Context()
        {
        }

        public ElectricStore1Context(DbContextOptions<ElectricStore1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Commodity> Commodities { get; set; } = null!;
        public virtual DbSet<CommodityCategory> CommodityCategories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Date> Dates { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123;database=ElectricStore1;trustservercertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CommodityId).HasColumnName("CommodityID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<Commodity>(entity =>
            {
                entity.ToTable("Commodity");

                entity.Property(e => e.CommodityId).HasColumnName("CommodityID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CommodityName).HasMaxLength(255);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Commodities)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Commodity__Categ__5629CD9C");
            });

            modelBuilder.Entity<CommodityCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Commodit__19093A2BA09ED59D");

                entity.ToTable("CommodityCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(255);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.CustomerAddress).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.CustomerPhone).HasMaxLength(20);

                entity.Property(e => e.CustomerType).HasMaxLength(50);

                entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.TotalBought).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Date>(entity =>
            {
                entity.ToTable("Date");

                entity.Property(e => e.DateId).HasColumnName("DateID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CommodityId).HasColumnName("CommodityID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateId).HasColumnName("DateID");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CommodityId)
                    .HasConstraintName("FK__Order__Commodity__571DF1D5");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__CustomerI__5812160E");

                entity.HasOne(d => d.Date)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DateId)
                    .HasConstraintName("FK__Order__DateID__49C3F6B7");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Order__StoreID__4AB81AF0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__UserID__4BAC3F29");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__4CA06362");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.Property(e => e.PromotionName).HasMaxLength(255);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.StoreName).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
