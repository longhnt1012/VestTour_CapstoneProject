using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VestTour.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddProductStyleOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankingAccount",
                columns: table => new
                {
                    BankingAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BankingA__A23083E52CD04698", x => x.BankingAccountID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryParentID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A2BAF30520F", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Fabric",
                columns: table => new
                {
                    FabricID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FabricName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fabric__3B1819CCFDB7948C", x => x.FabricID);
                });

            migrationBuilder.CreateTable(
                name: "Lining",
                columns: table => new
                {
                    LiningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LiningName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lining__82032A225C29625D", x => x.LiningID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE3A7C5056B0", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ShipperPartner",
                columns: table => new
                {
                    ShipperPartnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipperPartnerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShipperP__482D6AD0897CA8A1", x => x.ShipperPartnerID);
                });

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    StyleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Style__8AD147A0610756D3", x => x.StyleID);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    VoucherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VoucherCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DiscountNumber = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: true),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Voucher__3AEE79C1C7B07141", x => x.VoucherID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CCAC1AF6B505", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__User__RoleID__276EDEB3",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "StyleOption",
                columns: table => new
                {
                    StyleOptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleID = table.Column<int>(type: "int", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OptionValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StyleOpt__D69202C41FB42F9E", x => x.StyleOptionID);
                    table.ForeignKey(
                        name: "FK__StyleOpti__Style__48CFD27E",
                        column: x => x.StyleID,
                        principalTable: "Style",
                        principalColumn: "StyleID");
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    MeasurementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Neck = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Hip = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Waist = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Armhole = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Biceps = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PantsWaist = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Crotch = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Thigh = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PantsLength = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Measurem__85599F9824703E54", x => x.MeasurementID);
                    table.ForeignKey(
                        name: "FK__Measureme__UserI__2A4B4B5E",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankingAccountID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaymentDetails = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A5833503F87", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payment__Banking__3A81B327",
                        column: x => x.BankingAccountID,
                        principalTable: "BankingAccount",
                        principalColumn: "BankingAccountID");
                    table.ForeignKey(
                        name: "FK__Payment__UserID__3B75D760",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Store__3B82F0E19C15B6B2", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK__Store__UserID__2D27B809",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MeasurementID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    FabricID = table.Column<int>(type: "int", nullable: true),
                    LiningID = table.Column<int>(type: "int", nullable: true),
                    IsCustom = table.Column<bool>(type: "bit", nullable: true),
                    ImgURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__B40CC6ED51B9E1F7", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Product__Categor__5070F446",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Product__FabricI__5165187F",
                        column: x => x.FabricID,
                        principalTable: "Fabric",
                        principalColumn: "FabricID");
                    table.ForeignKey(
                        name: "FK__Product__LiningI__52593CB8",
                        column: x => x.LiningID,
                        principalTable: "Lining",
                        principalColumn: "LiningID");
                    table.ForeignKey(
                        name: "FK__Product__Measure__4F7CD00D",
                        column: x => x.MeasurementID,
                        principalTable: "Measurement",
                        principalColumn: "MeasurementID");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GuestName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GuestEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    GuestPhone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Booking__73951ACD0977B316", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK__Booking__StoreID__33D4B598",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK__Booking__UserID__32E0915F",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    VoucherID = table.Column<int>(type: "int", nullable: true),
                    ShipperPartnerID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ShippedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__C3905BAFE91436C0", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Order__PaymentID__3F466844",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "PaymentID");
                    table.ForeignKey(
                        name: "FK__Order__ShipperPa__4222D4EF",
                        column: x => x.ShipperPartnerID,
                        principalTable: "ShipperPartner",
                        principalColumn: "ShipperPartnerID");
                    table.ForeignKey(
                        name: "FK__Order__StoreID__403A8C7D",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK__Order__UserID__3E52440B",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Order__VoucherID__412EB0B6",
                        column: x => x.VoucherID,
                        principalTable: "Voucher",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "ProductStyleOption",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StyleOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductS__E965E6E3BF898D14", x => new { x.ProductId, x.StyleOptionId });
                    table.ForeignKey(
                        name: "FK__ProductSt__Produ__5535A963",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__ProductSt__Style__5629CD9C",
                        column: x => x.StyleOptionId,
                        principalTable: "StyleOption",
                        principalColumn: "StyleOptionID");
                });

            migrationBuilder.CreateTable(
                name: "ProductStyleOptions",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StyleOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStyleOptions", x => new { x.ProductId, x.StyleOptionId });
                    table.ForeignKey(
                        name: "FK_ProductStyleOptions_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductStyleOptions_StyleOption_StyleOptionId",
                        column: x => x.StyleOptionId,
                        principalTable: "StyleOption",
                        principalColumn: "StyleOptionID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateSubmitted = table.Column<DateOnly>(type: "date", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF6C40F5B8D", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Feedback__OrderI__5AEE82B9",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__Feedback__UserID__59FA5E80",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__08D097C3DC0885B2", x => new { x.OrderID, x.ProductId });
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__5DCAEF64",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Produ__5EBF139D",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_StoreID",
                table: "Booking",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserID",
                table: "Booking",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_OrderID",
                table: "Feedback",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserID",
                table: "Feedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_UserID",
                table: "Measurement",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentID",
                table: "Order",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShipperPartnerID",
                table: "Order",
                column: "ShipperPartnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreID",
                table: "Order",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserID",
                table: "Order",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherID",
                table: "Order",
                column: "VoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BankingAccountID",
                table: "Payment",
                column: "BankingAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserID",
                table: "Payment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FabricID",
                table: "Product",
                column: "FabricID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LiningID",
                table: "Product",
                column: "LiningID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MeasurementID",
                table: "Product",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStyleOption_StyleOptionId",
                table: "ProductStyleOption",
                column: "StyleOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStyleOptions_StyleOptionId",
                table: "ProductStyleOptions",
                column: "StyleOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UserID",
                table: "Store",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StyleOption_StyleID",
                table: "StyleOption",
                column: "StyleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D105341176EF89",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Voucher__7F0ABCA9B9062065",
                table: "Voucher",
                column: "VoucherCode",
                unique: true,
                filter: "[VoucherCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductStyleOption");

            migrationBuilder.DropTable(
                name: "ProductStyleOptions");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "StyleOption");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ShipperPartner");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Fabric");

            migrationBuilder.DropTable(
                name: "Lining");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropTable(
                name: "BankingAccount");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
