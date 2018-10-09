using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingApplication.Data.Models
{
    public partial class ShopingContext : DbContext
    {
        public ShopingContext()
        {
        }

        public ShopingContext(DbContextOptions<ShopingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorDistrict> VendorDistrict { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LTP-LUCIANV;Database=Shoping;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DistrictName).IsRequired();
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shops__IdDistric__1B0907CE");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.VendorName).IsRequired();
            });

            modelBuilder.Entity<VendorDistrict>(entity =>
            {
                entity.HasKey(e => new { e.IdVendor, e.IdDistrict });

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.VendorDistrict)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendorDis__IdDis__1ED998B2");

                entity.HasOne(d => d.IdVendorNavigation)
                    .WithMany(p => p.VendorDistrict)
                    .HasForeignKey(d => d.IdVendor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendorDis__IdVen__1DE57479");
            });
        }
    }
}
