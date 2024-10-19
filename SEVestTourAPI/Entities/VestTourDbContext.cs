using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Data;

public partial class VestTourDbContext : DbContext
{
    public VestTourDbContext()
    {
    }

    public VestTourDbContext(DbContextOptions<VestTourDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankingAccount> BankingAccounts { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Fabric> Fabrics { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Lining> Linings { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShipperPartner> ShipperPartners { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<StyleOption> StyleOptions { get; set; }
 

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-33UM2VQ;Database=VestTourDB;User Id=sa;Password=12345;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankingAccount>(entity =>
        {
            entity.HasKey(e => e.BankingAccountId).HasName("PK__BankingA__A23083E507021E0E");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDE4309503");

            entity.HasOne(d => d.Store).WithMany(p => p.Bookings).HasConstraintName("FK__Booking__StoreID__2A164134");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings).HasConstraintName("FK__Booking__UserID__29221CFB");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B456CC1B7");
        });

        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.HasKey(e => e.FabricId).HasName("PK__Fabric__3B1819CC0660D146");
        });
        modelBuilder.Entity<Fabric>()
            .Property(f => f.Tag)
            .HasConversion<string>();
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF674B6B0CB");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__OrderI__0F624AF8");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__UserID__0E6E26BF");
        });

        modelBuilder.Entity<Lining>(entity =>
        {
            entity.HasKey(e => e.LiningId).HasName("PK__Lining__82032A2241FE7509");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__Measurem__85599F98578466FC");

            entity.HasOne(d => d.User).WithMany(p => p.Measurements).HasConstraintName("FK__Measureme__UserI__2A4B4B5E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAFF53A51F8");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders).HasConstraintName("FK__Order__PaymentID__00200768");

            entity.HasOne(d => d.ShipperPartner).WithMany(p => p.Orders).HasConstraintName("FK__Order__ShipperPa__02FC7413");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("FK__Order__StoreID__01142BA1");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders).HasConstraintName("FK__Order__VoucherID__02084FDA");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A585F090078");

            entity.HasOne(d => d.BankingAccount).WithMany(p => p.Payments).HasConstraintName("FK__Payment__Banking__7C4F7684");

            entity.HasOne(d => d.User).WithMany(p => p.Payments).HasConstraintName("FK__Payment__UserID__7D439ABD");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED525EC021");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK__Product__Categor__208CD6FA");

            entity.HasOne(d => d.Fabric).WithMany(p => p.Products).HasConstraintName("FK__Product__FabricI__2180FB33");

            entity.HasOne(d => d.Lining).WithMany(p => p.Products).HasConstraintName("FK__Product__LiningI__22751F6C");

            entity.HasOne(d => d.Measurement).WithMany(p => p.Products).HasConstraintName("FK__Product__Measure__1F98B2C1");

            entity.HasOne(d => d.Order).WithMany(p => p.Products).HasConstraintName("FK__Product__OrderID__236943A5");

            entity.HasMany(d => d.StyleOptions).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductStyleOption",
                    r => r.HasOne<StyleOption>().WithMany()
                        .HasForeignKey("StyleOptionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSt__Style__3D2915A8"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSt__Produ__3C34F16F"),
                    j =>
                    {
                        j.HasKey("ProductId", "StyleOptionId").HasName("PK__ProductS__E965E6E3EF272F97");
                        j.ToTable("ProductStyleOption");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A177AF15D");
        });

        modelBuilder.Entity<ShipperPartner>(entity =>
        {
            entity.HasKey(e => e.ShipperPartnerId).HasName("PK__ShipperP__482D6AD0748327C8");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F0E1E45226C3");

            entity.HasOne(d => d.User).WithMany(p => p.Stores).HasConstraintName("FK__Store__UserID__71D1E811");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD147A07731577D");
        });

        modelBuilder.Entity<StyleOption>(entity =>
        {
            entity.HasKey(e => e.StyleOptionId).HasName("PK__StyleOpt__D69202C407AFB741");

            entity.HasOne(d => d.Style).WithMany(p => p.StyleOptions).HasConstraintName("FK__StyleOpti__Style__47DBAE45");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACEEC9EFA9");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__276EDEB3");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79C19ABBAB49");
        });



    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
