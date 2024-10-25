﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VestTour.Repository.Data;

#nullable disable

namespace VestTour.Repository.Migrations
{
    [DbContext(typeof(VestTourDbContext))]
    [Migration("20241024043412_AddProductStyleOption")]
    partial class AddProductStyleOption
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId")
                        .HasName("PK__OrderDet__08D097C3DC0885B2");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("ProductStyleOption", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StyleOptionId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "StyleOptionId")
                        .HasName("PK__ProductS__E965E6E3BF898D14");

                    b.HasIndex("StyleOptionId");

                    b.ToTable("ProductStyleOption", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.BankingAccount", b =>
                {
                    b.Property<int>("BankingAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BankingAccountID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BankingAccountId"));

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Bank")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("BankingAccountId")
                        .HasName("PK__BankingA__A23083E52CD04698");

                    b.ToTable("BankingAccount", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateOnly?>("BookingDate")
                        .HasColumnType("date");

                    b.Property<string>("GuestEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("GuestName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("GuestPhone")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    b.Property<TimeOnly?>("Time")
                        .HasColumnType("time");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("BookingId")
                        .HasName("PK__Booking__73951ACD0977B316");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<int?>("CategoryParentId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryParentID");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Category__19093A2BAF30520F");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Fabric", b =>
                {
                    b.Property<int>("FabricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FabricID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FabricId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FabricName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Tag")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("FabricId")
                        .HasName("PK__Fabric__3B1819CCFDB7948C");

                    b.ToTable("Fabric", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FeedbackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly?>("DateSubmitted")
                        .HasColumnType("date");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Response")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("FeedbackId")
                        .HasName("PK__Feedback__6A4BEDF6C40F5B8D");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Lining", b =>
                {
                    b.Property<int>("LiningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LiningID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LiningId"));

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<string>("LiningName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("LiningId")
                        .HasName("PK__Lining__82032A225C29625D");

                    b.ToTable("Lining", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Measurement", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MeasurementID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeasurementId"));

                    b.Property<decimal?>("Armhole")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Biceps")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Crotch")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Hip")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Neck")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("PantsLength")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("PantsWaist")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Thigh")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<decimal?>("Waist")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("MeasurementId")
                        .HasName("PK__Measurem__85599F9824703E54");

                    b.HasIndex("UserId");

                    b.ToTable("Measurement", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly?>("OrderDate")
                        .HasColumnType("date");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    b.Property<DateOnly?>("ShippedDate")
                        .HasColumnType("date");

                    b.Property<int?>("ShipperPartnerId")
                        .HasColumnType("int")
                        .HasColumnName("ShipperPartnerID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int?>("VoucherId")
                        .HasColumnType("int")
                        .HasColumnName("VoucherID");

                    b.HasKey("OrderId")
                        .HasName("PK__Order__C3905BAFE91436C0");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ShipperPartnerId");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<int?>("BankingAccountId")
                        .HasColumnType("int")
                        .HasColumnName("BankingAccountID");

                    b.Property<string>("Method")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly?>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("PaymentDetails")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payment__9B556A5833503F87");

                    b.HasIndex("BankingAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<int?>("FabricId")
                        .HasColumnType("int")
                        .HasColumnName("FabricID");

                    b.Property<string>("ImgUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImgURL");

                    b.Property<bool?>("IsCustom")
                        .HasColumnType("bit");

                    b.Property<int?>("LiningId")
                        .HasColumnType("int")
                        .HasColumnName("LiningID");

                    b.Property<int?>("MeasurementId")
                        .HasColumnType("int")
                        .HasColumnName("MeasurementID");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProductId")
                        .HasName("PK__Product__B40CC6ED51B9E1F7");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FabricId");

                    b.HasIndex("LiningId");

                    b.HasIndex("MeasurementId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.ProductStyleOption", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StyleOptionId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "StyleOptionId");

                    b.HasIndex("StyleOptionId");

                    b.ToTable("ProductStyleOptions");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId")
                        .HasName("PK__Role__8AFACE3A7C5056B0");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.ShipperPartner", b =>
                {
                    b.Property<int>("ShipperPartnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShipperPartnerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipperPartnerId"));

                    b.Property<string>("Company")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ShipperPartnerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ShipperPartnerId")
                        .HasName("PK__ShipperP__482D6AD0897CA8A1");

                    b.ToTable("ShipperPartner", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContactNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("StoreId")
                        .HasName("PK__Store__3B82F0E19C15B6B2");

                    b.HasIndex("UserId");

                    b.ToTable("Store", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Style", b =>
                {
                    b.Property<int>("StyleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StyleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StyleId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("StyleName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("StyleId")
                        .HasName("PK__Style__8AD147A0610756D3");

                    b.ToTable("Style", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.StyleOption", b =>
                {
                    b.Property<int>("StyleOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StyleOptionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StyleOptionId"));

                    b.Property<string>("OptionType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OptionValue")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("StyleId")
                        .HasColumnType("int")
                        .HasColumnName("StyleID");

                    b.HasKey("StyleOptionId")
                        .HasName("PK__StyleOpt__D69202C41FB42F9E");

                    b.HasIndex("StyleId");

                    b.ToTable("StyleOption", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly?>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("DOB");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__User__1788CCAC1AF6B505");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "UQ__User__A9D105341176EF89")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("VoucherID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherId"));

                    b.Property<DateOnly?>("DateEnd")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateStart")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("DiscountNumber")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VoucherCode")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("VoucherId")
                        .HasName("PK__Voucher__3AEE79C1C7B07141");

                    b.HasIndex(new[] { "VoucherCode" }, "UQ__Voucher__7F0ABCA9B9062065")
                        .IsUnique()
                        .HasFilter("[VoucherCode] IS NOT NULL");

                    b.ToTable("Voucher", (string)null);
                });

            modelBuilder.Entity("OrderDetail", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Order__5DCAEF64");

                    b.HasOne("VestTour.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Produ__5EBF139D");
                });

            modelBuilder.Entity("ProductStyleOption", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductSt__Produ__5535A963");

                    b.HasOne("VestTour.Domain.Entities.StyleOption", null)
                        .WithMany()
                        .HasForeignKey("StyleOptionId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductSt__Style__5629CD9C");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Booking", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Store", "Store")
                        .WithMany("Bookings")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Booking__StoreID__33D4B598");

                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Booking__UserID__32E0915F");

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Order", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Feedback__OrderI__5AEE82B9");

                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Feedback__UserID__59FA5E80");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Measurement", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Measurements")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Measureme__UserI__2A4B4B5E");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Order", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Payment", "Payment")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentId")
                        .HasConstraintName("FK__Order__PaymentID__3F466844");

                    b.HasOne("VestTour.Domain.Entities.ShipperPartner", "ShipperPartner")
                        .WithMany("Orders")
                        .HasForeignKey("ShipperPartnerId")
                        .HasConstraintName("FK__Order__ShipperPa__4222D4EF");

                    b.HasOne("VestTour.Domain.Entities.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Order__StoreID__403A8C7D");

                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Order__UserID__3E52440B");

                    b.HasOne("VestTour.Domain.Entities.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId")
                        .HasConstraintName("FK__Order__VoucherID__412EB0B6");

                    b.Navigation("Payment");

                    b.Navigation("ShipperPartner");

                    b.Navigation("Store");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Payment", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.BankingAccount", "BankingAccount")
                        .WithMany("Payments")
                        .HasForeignKey("BankingAccountId")
                        .HasConstraintName("FK__Payment__Banking__3A81B327");

                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Payment__UserID__3B75D760");

                    b.Navigation("BankingAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Product", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Product__Categor__5070F446");

                    b.HasOne("VestTour.Domain.Entities.Fabric", "Fabric")
                        .WithMany("Products")
                        .HasForeignKey("FabricId")
                        .HasConstraintName("FK__Product__FabricI__5165187F");

                    b.HasOne("VestTour.Domain.Entities.Lining", "Lining")
                        .WithMany("Products")
                        .HasForeignKey("LiningId")
                        .HasConstraintName("FK__Product__LiningI__52593CB8");

                    b.HasOne("VestTour.Domain.Entities.Measurement", "Measurement")
                        .WithMany("Products")
                        .HasForeignKey("MeasurementId")
                        .HasConstraintName("FK__Product__Measure__4F7CD00D");

                    b.Navigation("Category");

                    b.Navigation("Fabric");

                    b.Navigation("Lining");

                    b.Navigation("Measurement");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.ProductStyleOption", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Product", "Product")
                        .WithMany("ProductStyleOptions")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.HasOne("VestTour.Domain.Entities.StyleOption", "StyleOption")
                        .WithMany("ProductStyleOptions")
                        .HasForeignKey("StyleOptionId")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("StyleOption");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Store", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.User", "User")
                        .WithMany("Stores")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Store__UserID__2D27B809");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.StyleOption", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Style", "Style")
                        .WithMany("StyleOptions")
                        .HasForeignKey("StyleId")
                        .HasConstraintName("FK__StyleOpti__Style__48CFD27E");

                    b.Navigation("Style");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.User", b =>
                {
                    b.HasOne("VestTour.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__User__RoleID__276EDEB3");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.BankingAccount", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Fabric", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Lining", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Measurement", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Order", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Payment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductStyleOptions");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.ShipperPartner", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Store", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Style", b =>
                {
                    b.Navigation("StyleOptions");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.StyleOption", b =>
                {
                    b.Navigation("ProductStyleOptions");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");

                    b.Navigation("Measurements");

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("VestTour.Domain.Entities.Voucher", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
