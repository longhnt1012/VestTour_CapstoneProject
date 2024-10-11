using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Models;

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

    public virtual DbSet<ProductStyleOption> ProductStyleOptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShipperPartner> ShipperPartners { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<StyleOption> StyleOptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-QBI97SUM\\SQLEXPRESS;Initial Catalog=VestTourDB;Persist Security Info=True;User ID=sa;Password=12345;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankingAccount>(entity =>
        {
            entity.HasKey(e => e.BankingAccountId).HasName("PK__BankingA__A23083E575B7927C");

            entity.ToTable("BankingAccount");

            entity.Property(e => e.BankingAccountId).HasColumnName("BankingAccountID");
            entity.Property(e => e.AccountName).HasMaxLength(255);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bank).HasMaxLength(255);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD6DD04B75");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

            entity.HasOne(d => d.Store).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Booking__StoreID__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserID__44FF419A");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK__Booking__Voucher__45F365D3");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B339FA8ED");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryParentId).HasColumnName("CategoryParentID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.HasKey(e => e.FabricId).HasName("PK__Fabric__3B1819CCFD1AB4D9");

            entity.ToTable("Fabric");

            entity.Property(e => e.FabricId).HasColumnName("FabricID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FabricName).HasMaxLength(255);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF68DC2E656");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Response).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feedback__OrderI__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__6C190EBB");
        });

        modelBuilder.Entity<Lining>(entity =>
        {
            entity.HasKey(e => e.LiningId).HasName("PK__Lining__82032A22E4797885");

            entity.ToTable("Lining");

            entity.Property(e => e.LiningId).HasColumnName("LiningID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LiningName).HasMaxLength(255);
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__Measurem__85599F98B74F82DA");

            entity.ToTable("Measurement");

            entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
            entity.Property(e => e.Armhole).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Biceps).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Crotch).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Hip).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Neck).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PantsLength).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PantsWaist).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Thigh).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Waist).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Measureme__UserI__3C69FB99");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF1C91DCB8");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.ShipperPartnerId).HasColumnName("ShipperPartnerID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Order__PaymentID__5165187F");

            entity.HasOne(d => d.ShipperPartner).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperPartnerId)
                .HasConstraintName("FK__Order__ShipperPa__5441852A");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Order__StoreID__52593CB8");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK__Order__VoucherID__534D60F1");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A58E1CC4657");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BankingAccountId).HasColumnName("BankingAccountID");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.PaymentDetails).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BankingAccount).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BankingAccountId)
                .HasConstraintName("FK__Payment__Banking__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserID__4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED6003DC80");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.FabricId).HasColumnName("FabricID");
            entity.Property(e => e.LiningId).HasColumnName("LiningID");
            entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductCode).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__628FA481");

            entity.HasOne(d => d.Fabric).WithMany(p => p.Products)
                .HasForeignKey(d => d.FabricId)
                .HasConstraintName("FK__Product__FabricI__6383C8BA");

            entity.HasOne(d => d.Lining).WithMany(p => p.Products)
                .HasForeignKey(d => d.LiningId)
                .HasConstraintName("FK__Product__LiningI__6477ECF3");

            entity.HasOne(d => d.Measurement).WithMany(p => p.Products)
                .HasForeignKey(d => d.MeasurementId)
                .HasConstraintName("FK__Product__Measure__619B8048");

            entity.HasOne(d => d.Order).WithMany(p => p.Products)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Product__OrderID__656C112C");
        });

        modelBuilder.Entity<ProductStyleOption>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductStyleOption");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.StyleOptionId).HasColumnName("StyleOptionID");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductSt__Produ__6754599E");

            entity.HasOne(d => d.StyleOption).WithMany()
                .HasForeignKey(d => d.StyleOptionId)
                .HasConstraintName("FK__ProductSt__Style__68487DD7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AC604CBF6");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<ShipperPartner>(entity =>
        {
            entity.HasKey(e => e.ShipperPartnerId).HasName("PK__ShipperP__482D6AD06EEA6CF7");

            entity.ToTable("ShipperPartner");

            entity.Property(e => e.ShipperPartnerId).HasColumnName("ShipperPartnerID");
            entity.Property(e => e.Company).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShipperPartnerName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F0E1B883CB30");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Stores)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Store__UserID__3F466844");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD147A0EC8F2B6A");

            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.StyleName).HasMaxLength(255);
        });

        modelBuilder.Entity<StyleOption>(entity =>
        {
            entity.HasKey(e => e.StyleOptionId).HasName("PK__StyleOpt__D69202C43EC25558");

            entity.ToTable("StyleOption");

            entity.Property(e => e.StyleOptionId).HasColumnName("StyleOptionID");
            entity.Property(e => e.OptionType).HasMaxLength(100);
            entity.Property(e => e.OptionValue).HasMaxLength(100);
            entity.Property(e => e.StyleId).HasColumnName("StyleID");

            entity.HasOne(d => d.Style).WithMany(p => p.StyleOptions)
                .HasForeignKey(d => d.StyleId)
                .HasConstraintName("FK__StyleOpti__Style__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACEF9CFCA9");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105343FBA8525").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__398D8EEE");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79C1B49BA835");

            entity.ToTable("Voucher");

            entity.HasIndex(e => e.VoucherCode, "UQ__Voucher__7F0ABCA9A19FB368").IsUnique();

            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DiscountNumber).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
