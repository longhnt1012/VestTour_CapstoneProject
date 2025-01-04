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

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<ProcessingTailor> ProcessingTailors { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductInStore> ProductInStores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShipperPartner> ShipperPartners { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<StyleOption> StyleOptions { get; set; }

    public virtual DbSet<TailorPartner> TailorPartners { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=SQL8003.site4now.net;Database=db_aaf855_vesttourdb;User Id=db_aaf855_vesttourdb_admin;Password=deployDBv1#;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankingAccount>(entity =>
        {
            entity.HasKey(e => e.BankingAccountId).HasName("PK__BankingA__A23083E52CD04698");

            entity.ToTable("BankingAccount");

            entity.Property(e => e.BankingAccountId).HasColumnName("BankingAccountID");
            entity.Property(e => e.AccountName).HasMaxLength(255);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bank).HasMaxLength(255);
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

            entity.HasOne(d => d.Payment).WithMany(p => p.BankingAccounts)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__BankingAc__Payme__662B2B3B");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD0977B316");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.AssistStaffName).HasMaxLength(255);
            entity.Property(e => e.GuestEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GuestName).HasMaxLength(255);
            entity.Property(e => e.GuestPhone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Service).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Store).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Booking__StoreID__33D4B598");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserID__32E0915F");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2BAF30520F");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryParentId).HasColumnName("CategoryParentID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.HasKey(e => e.FabricId).HasName("PK__Fabric__3B1819CCFDB7948C");

            entity.ToTable("Fabric");

            entity.Property(e => e.FabricId).HasColumnName("FabricID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FabricName).HasMaxLength(255);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Tag).HasMaxLength(255);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6C40F5B8D");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Response).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feedback__OrderI__5AEE82B9");

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Feedback__Produc__756D6ECB");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__59FA5E80");
        });

        modelBuilder.Entity<Lining>(entity =>
        {
            entity.HasKey(e => e.LiningId).HasName("PK__Lining__82032A225C29625D");

            entity.ToTable("Lining");

            entity.Property(e => e.LiningId).HasColumnName("LiningID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LiningName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__Measurem__85599F9824703E54");

            entity.ToTable("Measurement");

            entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
            entity.Property(e => e.Armhole).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Biceps).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Chest).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Crotch).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Hip).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.JacketLength).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Neck).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PantsLength).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PantsWaist).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Shoulder).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SleeveLength).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Thigh).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Waist).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Measureme__UserI__2A4B4B5E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAFE91436C0");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BalancePayment).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DeliveryMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Deposit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GuestAddress).HasMaxLength(255);
            entity.Property(e => e.GuestPhone).HasMaxLength(12);
            entity.Property(e => e.GuestEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GuestName).HasMaxLength(255);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.RevenueShare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ShipStatus).HasMaxLength(50);
            entity.Property(e => e.ShipperPartnerId).HasColumnName("ShipperPartnerID");
            entity.Property(e => e.ShippingFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

            entity.HasOne(d => d.ShipperPartner).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperPartnerId)
                .HasConstraintName("FK__Order__ShipperPa__0697FACD");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Order__StoreID__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Order__UserID__3E52440B");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK__Order__VoucherID__412EB0B6");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderDet__08D097C3DC0885B2");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__5DCAEF64");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__5EBF139D");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5833503F87");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDetails).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__OrderID__74794A92");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserID__3B75D760");
        });

        modelBuilder.Entity<ProcessingTailor>(entity =>
        {
            entity.HasKey(e => e.ProcessingId).HasName("PK__Processi__22A4D72E47642706");

            entity.ToTable("ProcessingTailor");

            entity.Property(e => e.ProcessingId).HasColumnName("ProcessingID");
            entity.Property(e => e.DeliveryStatus).HasMaxLength(50);
            entity.Property(e => e.FixStatus).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.SampleStatus).HasMaxLength(50);
            entity.Property(e => e.StageName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TailorPartnerId).HasColumnName("TailorPartnerID");

            entity.HasOne(d => d.Order).WithMany(p => p.ProcessingTailors)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Processin__Order__56E8E7AB");

            entity.HasOne(d => d.TailorPartner).WithMany(p => p.ProcessingTailors)
                .HasForeignKey(d => d.TailorPartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Processin__Tailo__55F4C372");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED51B9E1F7");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.FabricId).HasColumnName("FabricID");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("ImgURL");
            entity.Property(e => e.LiningId).HasColumnName("LiningID");
            entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCode).HasMaxLength(100);
            entity.Property(e => e.Size)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Size");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__5070F446");

            entity.HasOne(d => d.Fabric).WithMany(p => p.Products)
                .HasForeignKey(d => d.FabricId)
                .HasConstraintName("FK__Product__FabricI__5165187F");

            entity.HasOne(d => d.Lining).WithMany(p => p.Products)
                .HasForeignKey(d => d.LiningId)
                .HasConstraintName("FK__Product__LiningI__52593CB8");

            entity.HasOne(d => d.Measurement).WithMany(p => p.Products)
                .HasForeignKey(d => d.MeasurementId)
                .HasConstraintName("FK__Product__Measure__4F7CD00D");

            entity.HasMany(d => d.StyleOptions).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductStyleOption",
                    r => r.HasOne<StyleOption>().WithMany()
                        .HasForeignKey("StyleOptionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSt__Style__5629CD9C"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSt__Produ__5535A963"),
                    j =>
                    {
                        j.HasKey("ProductId", "StyleOptionId").HasName("PK__ProductS__E965E6E3BF898D14");
                        j.ToTable("ProductStyleOption");
                    });
        });

        modelBuilder.Entity<ProductInStore>(entity =>
        {
            entity.ToTable("ProductInStore");
            entity.HasKey(e => new { e.StoreId, e.ProductId });

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");
        

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__625A9A57");

            entity.HasOne(d => d.Store).WithMany()
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Store__6166761E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A7C5056B0");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<ShipperPartner>(entity =>
        {
            entity.HasKey(e => e.ShipperPartnerId).HasName("PK__ShipperP__482D6AD0897CA8A1");

            entity.ToTable("ShipperPartner");

            entity.Property(e => e.ShipperPartnerId).HasColumnName("ShipperPartnerID");
            entity.Property(e => e.Company).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F0E19C15B6B2");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.ImgUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StaffIds).HasColumnName("StaffIDs");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Stores)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Store__UserID__2D27B809");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD147A0610756D3");

            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StyleName).HasMaxLength(255);
        });

        modelBuilder.Entity<StyleOption>(entity =>
        {
            entity.HasKey(e => e.StyleOptionId).HasName("PK__StyleOpt__D69202C41FB42F9E");

            entity.ToTable("StyleOption");

            entity.Property(e => e.StyleOptionId).HasColumnName("StyleOptionID");
            entity.Property(e => e.OptionType).HasMaxLength(100);
            entity.Property(e => e.OptionValue).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StyleId).HasColumnName("StyleID");

            entity.HasOne(d => d.Style).WithMany(p => p.StyleOptions)
                .HasForeignKey(d => d.StyleId)
                .HasConstraintName("FK__StyleOpti__Style__48CFD27E");
        });

        modelBuilder.Entity<TailorPartner>(entity =>
        {
            entity.HasKey(e => e.TailorPartnerId).HasName("PK__TailorPa__92F307536A3D31BC");

            entity.ToTable("TailorPartner");

            entity.HasIndex(e => e.StoreId, "UQ__TailorPa__3B82F0E018BF59DE").IsUnique();

            entity.Property(e => e.TailorPartnerId).HasColumnName("TailorPartnerID");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Store).WithOne(p => p.TailorPartner)
                .HasForeignKey<TailorPartner>(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TailorPar__Store__503BEA1C");

            entity.HasOne(d => d.User).WithMany(p => p.TailorPartners)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TailorPar__UserI__73852659");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC1AF6B505");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105341176EF89").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AvtUrl).HasMaxLength(255);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__276EDEB3");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79C1C7B07141");

            entity.ToTable("Voucher");

            entity.HasIndex(e => e.VoucherCode, "UQ__Voucher__7F0ABCA9B9062065").IsUnique();

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
