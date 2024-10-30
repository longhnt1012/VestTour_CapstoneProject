
--USE master;
--ALTER DATABASE VestTourDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE VestTourDB;
CREATE DATABASE VestTourDB;
GO

USE VestTourDB;
GO
CREATE TABLE [Role] (
    RoleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(100)  NOT NULL
);
INSERT INTO Role (RoleName)
VALUES 
    ('admin'),
    ('staff'),
    ('customer'),
    ('store manager');
	
CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(255)  NOT NULL,
    Gender NVARCHAR(10),
    Address NVARCHAR(255),
    DOB DATE,
	RoleID INT FOREIGN KEY REFERENCES [Role](RoleID)  NOT NULL, 
    Email NVARCHAR(255) UNIQUE  NOT NULL,
    Password NVARCHAR(255)  NOT NULL,
	Phone VARCHAR(11),
	[Status] VARCHAR(50),
    IsConfirmed BIT  NOT NULL,
	RefreshToken NVARCHAR(MAX) NULL,
    RefreshTokenExpiryTime DATETIME NULL
);



INSERT INTO [User] (Name, Gender, Address, DOB, RoleID, Email, Password,IsConfirmed)
VALUES 
    ('Thanh Long', 'Male', '111 Tran Hung Dao St', '2001-01-01', 1, 'admin123@gmail.com', '123456',1),
	('Minh Nhut', 'Female', 'S606B Vinhome GrandPark , Nguyen Xien St, Thu Duc city', '2002-02-15', 3, 'customer111@gmail.com', '111111',1),
    ('Nguyen Van A', 'Male', '222 Le Loi St, Ho Chi Minh City', '1998-05-20', 2, 'staff1@gmail.com', 'staffpass1', 1),
    ('Tran Thi B', 'Female', '333 Hai Ba Trung St, Da Nang', '1999-07-15', 2, 'staff2@gmail.com', 'staffpass2', 1),
	('Hoang Van E', 'Male', '666 Ly Thuong Kiet St, Nha Trang', '1996-03-30', 4, 'manager2@gmail.com', 'managerpass2', 1),
	('Hoang Thien Phuc', 'Male', '243 Tran Hung Dao St, Long Xuyen City, An Giang', '2002-07-15', 3, 'phuchoang@gmail.com', '123',1),
	('Lam Thi Thuy Nga', 'Female', '11 Ly Thai To St, Long Xuyen City, An Giang', '1997-07-15', 4, 'thuynga@gmail.com', '123111',1);

UPDATE [User] SET [Status]='active'
UPDATE [User] SET Phone='0911945965'
-- Table: Measurement
CREATE TABLE Measurement (
    MeasurementID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    Weight DECIMAL(5,2),
    Height DECIMAL(5,2),
    Neck DECIMAL(5,2),
    Hip DECIMAL(5,2),
    Waist DECIMAL(5,2),
    Armhole DECIMAL(5,2),
    Biceps DECIMAL(5,2),
    PantsWaist DECIMAL(5,2),
    Crotch DECIMAL(5,2),
    Thigh DECIMAL(5,2),
    PantsLength DECIMAL(5,2)
);
ALTER TABLE Measurement
ADD Age INT,
    Chest DECIMAL(5,2),
    Shoulder DECIMAL(5,2),
    SleeveLength DECIMAL(5,2),
    JacketLength DECIMAL(5,2);

INSERT INTO [Measurement] 
(UserID, Weight, Height, Neck, Hip, Waist, Armhole, Biceps, PantsWaist, Crotch, Thigh, PantsLength) 
VALUES 
(2, 75.00, 175.00, 40.00, 95.00, 85.00, 45.00, 35.00, 90.00, 30.00, 55.00, 100.00),
(6, 80.00, 180.00, 45.00, 98.00, 88.00, 42.00, 37.00, 87.00, 35.00, 53.00, 103.00);
-- Table: Store
DROP TABLE IF EXISTS Store;
GO
CREATE TABLE Store (
    StoreID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255),
    ContactNumber VARCHAR(20),
   
);
INSERT INTO [Store] (UserID,Name,Address,ContactNumber)
VALUES 
	(5,'Vest pro',' 441/2/2G Quang Trung, P.10, Q.Gò Vấp, TP.HCM','0169136419'),
	(7,'Vest Nu Cao Cap','349 Minh Khai, Vinh Tuy, Ha Noi','02437473999');

-- Table: Voucher
CREATE TABLE Voucher (
    VoucherID INT PRIMARY KEY IDENTITY(1,1),
    [Status] NVARCHAR(50),
    VoucherCode VARCHAR(100) UNIQUE,
    [Description] NVARCHAR(255),
    DiscountNumber DECIMAL(5,2),
    DateStart DATE,
    DateEnd DATE
);

INSERT INTO Voucher (Status, VoucherCode, [Description], DiscountNumber, DateStart, DateEnd)
VALUES 
    ('On going', 'FREESHIP20', 'Discount 20% shipping fee. 20 years anniversary', 0.2, '2024-09-29', '2024-09-30'),
	('On going', 'BIGSALE30', 'Discount 30% on total price. golden hour', 0.2, '2024-10-20', '2024-10-25');

-- Table: Booking
DROP TABLE IF EXISTS Booking;
GO
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    BookingDate DATE,
	GuestName NVARCHAR(255),
	GuestEmail VARCHAR(255),
	GuestPhone VARCHAR(11),
    Time TIME,
    Note NVARCHAR(255),
    Status NVARCHAR(50),
    StoreID INT FOREIGN KEY REFERENCES Store(StoreID),
	DepositCost DECIMAL(10,2) NULL,
    [Service] NVARCHAR(255) 
);

--Table : Shipperpartner
CREATE TABLE ShipperPartner(
	ShipperPartnerID INT PRIMARY KEY IDENTITY(1,1),
	ShipperPartnerName NVARCHAR(255) NOT NULL,
	Phone VARCHAR(20),
	Company NVARCHAR(255),
	Status NVARCHAR(50),
)
INSERT INTO ShipperPartner (ShipperPartnerName, Phone, Company, Status)
VALUES
	('Le Hong Ngoc','0914721438','VNEXPRESS','Success'),
	('Bui Thanh Hai','0918253644','GHTK','Success');

-- Table: BankingAccount
DROP TABLE IF EXISTS BankingAccount;
GO
CREATE TABLE BankingAccount (
    BankingAccountID INT PRIMARY KEY IDENTITY(1,1),
    AccountNumber VARCHAR(50) NOT NULL,
    AccountName NVARCHAR(255) NOT NULL,
    Bank NVARCHAR(255)
);
-- Table: Payment
DROP TABLE IF EXISTS Payment;
GO
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
	BankingAccountID INT FOREIGN KEY REFERENCES BankingAccount(BankingAccountID),
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
    Method NVARCHAR(50),
    PaymentDate DATE,
    PaymentDetails NVARCHAR(255),
    Status NVARCHAR(50)
   
);
-- Table: Order
DROP TABLE IF EXISTS [Order];
GO
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
	PaymentID INT FOREIGN KEY REFERENCES Payment(PaymentID),
   StoreID INT FOREIGN KEY REFERENCES Store(StoreID),
   VoucherID INT FOREIGN KEY REFERENCES Voucher(VoucherID),
   ShipperPartnerID INT FOREIGN KEY REFERENCES ShipperPartner(ShipperPartnerID),
    OrderDate DATE,
    ShippedDate DATE,
    Note NVARCHAR(255),
    Paid BIT NOT NULL, 
	Status NVARCHAR(50),
	TotalPrice DECIMAL(10,2),
	Deposit DECIMAL(10,2) NULL,
    BalancePayment DECIMAL(10,2) 
); 

-------------CHAY CAI NAY THOI --------------------

-- Insert into BankingAccount
INSERT INTO BankingAccount (AccountNumber, AccountName, Bank)
VALUES ('123456789', 'Tran Dinh Nam', 'ABC Bank'),
('017102006833', 'Le Huynh Van Anh', 'TP Bank');

-- Insert into Payment
INSERT INTO Payment (BankingAccountID, UserID, Method, PaymentDate, PaymentDetails, Status)
VALUES 
(1, 2, 'Visa Card', '2024-10-02', 'Payment for order #1', 'Completed'),
(1, 6, 'Momo', '2024-10-03', 'Payment for order #2', 'Completed'),
(1, 6, 'Momo', '2024-10-20', 'Payment for order #3', 'Completed');
-- Insert into [Order]
INSERT INTO [Order] (PaymentID,UserID, StoreID, VoucherID, ShipperPartnerID, OrderDate, ShippedDate, Note, Paid, Status,TotalPrice)
VALUES 
(1,1, 1, null, 1, '2024-10-02', '2024-10-17', 'Urgent delivery', 1, 'Shipped',650),
(2,2, 1, null, 2, '2024-10-03', '2024-10-18', 'Ship COD', 1, 'Shipped',300),
(2,2, 1, null, 2, '2024-10-19', '2024-10-21', 'Ship COD', 1, 'Shipped',450);

-- Table: Fabric
CREATE TABLE Fabric (
    FabricID INT PRIMARY KEY IDENTITY(1,1),
    FabricName NVARCHAR(255),
    Price DECIMAL(10,2),
    Description NVARCHAR(255),
    ImageURL NVARCHAR(255),
	Tag NVARCHAR(255)	
);
-------------UPDATE FABRIC TABLE
INSERT INTO Fabric (FabricName, Price, Description) VALUES
('577', 350, '100% Wool'),
('576', 350, '100% Wool'),
('1268', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1267', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1266', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1265', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1264', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1263', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1262', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1261', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('1260', 250, '60% Wool . 20% Cashmere . 20% Synthetic'),
('193', 200, '50% Wool . 30% Cashmere . 20% Synthetic'),
('492', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('493', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('494', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('495', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('485', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('486', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('487', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('488', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('489', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('490', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('491', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('496', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('191', 200, '50% Wool . 30% Cashmere . 20% Synthetic'),
('190', 200, '50% Wool . 30% Cashmere . 20% Synthetic'),
('497', 220, '50% Wool . 30% Cashmere . 20% Synthetic'),
('1557', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('733', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('732', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('731', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('730', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('729', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('728', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('727', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('726', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('725', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('724', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('723', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('722', 220, '20% Cashmere . 60% Wool . 20% Synthetic'),
('656', 270, '60% Wool . 30% Cashmere'),
('664', 270, '50% Cashmere . 40% Wool'),
('663', 270, '50% Cashmere . 40% Wool'),
('666', 270, '50% Cashmere . 40% Wool'),
('665', 270, '50% Cashmere . 40% Wool'),
('660', 270, '50% Cashmere . 40% Wool'),
('661', 270, '50% Cashmere . 40% Wool'),
('655', 270, '60% Wool . 30% Cashmere'),
('17', 220, '50% Wool - 30% Cashmere'),
('654', 270, '60% Wool . 30% Cashmere'),
('662', 270, '50% Cashmere . 40% Wool'),
('36', 280, 'Wool 85% . Cashmere 10%'),
('1145', 250, 'Wool 80% . Cashmere 10%'),
('35', 280, 'Wool 80% . Cashmere 15%'),
('192', 200, 'Cashmere: 85% . Wool: 10%'),
('234', 220, 'Cashmere: 85% . Wool: 10%'),
('81', 150, 'Wool 60% . Cashmere 20%'),
('1354', 250, 'Wool 80% . Cashmere 10%'),
('176', 200, 'Wool 80% . Cashmere 10%'),
('1356', 250, '85% Wool . 10% Cashmere'),
('253', 200, 'Cashmere: 80% . Wool: 10%'),
('533', 400, '95% Wool'),
('183', 200, 'Wool 80% . Cashmere 10%'),
('182', 200, 'Wool 80% . Cashmere 10%'),
('675', 270, 'Cashmere: 80% . Wool: 10%'),
('574', 350, '100% Wool'),
('408', 350, '85% Wool . 10% Cashmere'),
('344', 250, 'Wool 80% . Cashmere 15%'),
('1144', 250, 'Wool 85% . Cashmere 10%'),
('1462', 230, '65% Wool . 10% Cashmere'),
('69', 250, 'Cashmere: 20% . Wool: 80%'),
('178', 200, 'Wool 80% . Cashmere 10%'),
('800', 280, 'Wool 65% . Cashmere 20%'),
('93', 220, 'Wool 80% . Cashmere 15%'),
('351', 220, 'Wool 80% . Cashmere 15%'),
('1358', 220, '85% Wool . 10% Cashmere'),
('1355', 220, '85% Wool . 10% Cashmere'),
('175', 200, 'Wool 80% . Cashmere 10%'),
('87', 200, 'Wool 80% . Cashmere 10%'),
('1366', 280, 'Wool 85% . Cashmere 10%'),
('181', 200, 'Wool 80% . Cashmere 10%'),
('1143', 250, 'Wool 80% . Cashmere 10%'),
('88', 270, '85% Wool . 10% Cashmere Blue'),
('573', 350, '100% Wool'),
('890', 270, 'Wool 85% . Cashmere 10%'),
('24', 200, 'Wool 85% . Cashmere 10%'),
('63', 220, 'Cashmere: 15% . Wool: 75%'),
('64', 250, 'Cashmere: 15% . Wool: 75%'),
('66', 250, 'Cashmere: 15% . Wool: 75%'),
('62', 250, 'Cashmere: 15% . Wool: 75%'),
('1318', 200, 'Wool 80% . Cashmere 10%'),
('895', 270, 'Wool 85% . Cashmere 10%'),
('801', 170, 'Wool 50% . Cashmere 35%'),
('673', 270, 'Cashmere: 20% . Wool: 80%'),
('96', 220, 'Wool 80% . Cashmere 15%'),
('1460', 200, 'Wool 85% . Cashmere 10%'),
('1545', 290, 'Wool 85% . Cashmere 10%'),
('1113', 170, 'Wool 65% . Cashmere 20%'),
('38', 270, '85% Wool . 10% Cashmere'),
('1256', 300, 'Wool 85% . Cashmere 10%'),
('1080', 220, '65% Wool . 10% Cashmere'),
('538', 320, 'Wool 100% . Cashmere 0%'),
('556', 250, 'Wool: 60% . Cashmere: 20% . Synthetic: 20%'),
('1374', 270, 'Wool 85% . Cashmere 10%'),
('930', 270, 'Wool 85% . Cashmere 10%'),
('723', 350, '100% Wool'),
('39', 270, '85% Wool . 10% Cashmere'),
('665', 220, '85% Wool . 10% Cashmere');
UPDATE Fabric SET Tag = 'Premium'
UPDATE Fabric
SET Tag = 'New'
WHERE FabricID % 3 = 0;
UPDATE Fabric
SET Tag = 'Sale'
WHERE FabricID % 7 = 0;
UPDATE Fabric
SET ImageURL = CASE FabricID
	WHEN 1 THEN 'https://www.skillshare.com/blog/wp-content/uploads/2024/07/QDfyOOeEnmY1FTVzdlkiJQJyC6wcH-Z6MssXzygnrPmB8ed5PG1O9clFzPsZi90ckgU-C-TgcTf-3oWlHdVcJmsPgK7zUzOg_IHx9_bKJosfZ7M0GFihWip-OINpM1hW1hYy2ej.png'
    WHEN 2 THEN 'https://a.storyblok.com/f/165154/1456x816/c0fa2fe609/02_-cotton-fabric.png/m/'
    WHEN 3 THEN 'https://cdn.shopify.com/s/files/1/0573/2689/5255/files/20230419_172105_480x480.jpg?v=1699502117'
    WHEN 4 THEN 'https://s3.eu-west-2.amazonaws.com/files.sewport.com/fabrics-directory/what-is-crepe-fabric-properties-how-its-made-and-where/Casa%20Collection%20Crepe%20Fabric.jpg'
    WHEN 5 THEN 'https://www.shutterstock.com/shutterstock/videos/1074764369/thumb/1.jpg?ip=x480'
    WHEN 6 THEN 'https://www.skillshare.com/blog/wp-content/uploads/2024/07/Ilcn6Yf8n_eSzzMcAh_X2shvPYaITPLjVezcCnZS3CLVDD6p_eiI1po-HuXDsWUbE6BdCT1OgQJ3SRM-xSpSB-2qstFjyo6lLVl047D2QQfu-7iXLByEgZw3r9LlEXFUShLiUDfi.png'
    WHEN 7 THEN 'http://t1.gstatic.com/images?q=tbn:ANd9GcSDlDq0BtL2KNyghOj5GsWHc1ZH2s9_xGdnODoZwCnpIaTo8k-O'
    WHEN 8 THEN 'https://lindleygeneralstore.ca/cdn/shop/products/1014-1467_2f057ec1-1981-41dc-842d-345d6037ce1b.jpg?v=1570153375&width=432'
    WHEN 9 THEN 'https://t2.gstatic.com/licensed-image?q=tbn:ANd9GcSpMB7eCfnezGjv7wI9dZ9uPRhcmfxr1bW5wh-McT6XvelYXNn8vlUD9p_YQwjmfTyC'
	WHEN 10 THEN 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1OIrpNsmc3-xKTA09moAN-BM_EX1KOfz5frimjq-a-Ha2uRGmhayL8FjZ0_VHu22mj-8&usqp=CAU'
	WHEN 11 THEN 'https://brydenapparel.com/wp-content/uploads/2023/07/Georgette-Fabric-4.jpg'
    WHEN 12 THEN 'https://images.squarespace-cdn.com/content/v1/5d5c66a4dc77140001f49b0d/fda98968-2f53-4110-9901-5bbd91d04855/tawna+lawn.jpg'
    WHEN 13 THEN 'https://cdn.britannica.com/34/123834-050-45AF15D2/Cotton-corduroy.jpg'
    WHEN 14 THEN 'https://www.tradeuno.com/cdn/shop/files/IMG_E8323.jpg?v=1697199814&width=1946'
    ELSE ImageURL
END
WHERE FabricID IN (2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14);

-- Table: Style
CREATE TABLE Style (
    StyleID INT PRIMARY KEY IDENTITY(1,1),
    StyleName NVARCHAR(255),
    Description NVARCHAR(255)
);
-- Insert styles into Style table
INSERT INTO Style (StyleName, Description) VALUES
('Jacket', 'Various jacket styles and options'),
('Pants', 'Various pants styles and options'),
('Vest', 'Various vest styles and options');

-- Table: StyleOption
CREATE TABLE StyleOption (
    StyleOptionID INT PRIMARY KEY IDENTITY(1,1),
	StyleID INT FOREIGN KEY REFERENCES Style(StyleID),
    OptionType NVARCHAR(100),
    OptionValue NVARCHAR(100),
	 Price DECIMAL(10,2)
);

-- Insert options for Jacket
INSERT INTO StyleOption (StyleID, OptionType, OptionValue) VALUES
(1, 'Style', 'single-breasted 1 button'),
(1, 'Style', 'single-breasted 2 button'),
(1, 'Style', 'single-breasted 3 button'),
(1, 'Style', 'double-breasted 2 button'),
(1, 'Style', 'double-breasted 4 button'),
(1, 'Style', 'double-breasted 6 button'),
(1, 'Style', 'mandarin'),
(1, 'Fit', 'Slim fit'),
(1, 'Fit', 'Regular'),
(1, 'Jacket lapels', 'Norch'),
(1, 'Jacket lapels', 'Peak'),
(1, 'Jacket lapels', 'Shawl'),
(1, 'Lapel Width', 'Slim'),
(1, 'Lapel Width', 'Standard'),
(1, 'Lapel Width', 'Wide'),
(1, 'Pocket style', 'No pocket'),
(1, 'Pocket style', 'With Flap'),
(1, 'Pocket style', 'Double-Welted'),
(1, 'Pocket style', 'Patched'),
(1, 'Pocket style', 'With flap x3'),
(1, 'Pocket style', 'Double-Welted x3'),
(1, 'Slant', 'standard'),
(1, 'Slant', 'slanted'),
(1, 'Sleeve buttons', '0'),
(1, 'Sleeve buttons', '2'),
(1, 'Sleeve buttons', '3'),
(1, 'Sleeve buttons', '4'),
(1, 'Back style', 'Ventless'),
(1, 'Back style', 'Center Vent'),
(1, 'Back style', 'Side Vent'),
(1, 'Breast pocket', 'No'),
(1, 'Breast pocket', 'Yes'),
(1, 'Breast pocket', 'Patched'),
(1, 'Breast pocket', 'Patched x2');

-- Insert options for Pants
INSERT INTO StyleOption (StyleID, OptionType, OptionValue) VALUES
(2, 'fit', 'slim fit'),
(2, 'fit', 'regular fit'),
(2, 'pleats', 'no pleats'),
(2, 'pleats', 'pleated'),
(2, 'pleats', 'Double pleats'),
(2, 'Pants fastening', 'Centered'),
(2, 'Pants fastening', 'Displaced'),
(2, 'Pants fastening', 'No button'),
(2, 'Pants fastening', 'Off-centered: Buttonless'),
(2, 'Side pockets', 'Diagonal'),
(2, 'Side pockets', 'Vertical'),
(2, 'Side pockets', 'Rounded'),
(2, 'Back pockets', 'No Pockets'),
(2, 'Back pockets', 'Double-Welted Pocket With Button'),
(2, 'Back pockets', 'Patched'),
(2, 'Back pockets', 'Flap Pockets'),
(2, 'Back pockets', 'Double-Welted Pocket With Button x2'),
(2, 'Back pockets', 'Patched x2'),
(2, 'Pant Cuffs', 'No pant cuffs'),
(2, 'Pant Cuffs', 'With pant cuffs');
-- Insert options for Vest
INSERT INTO StyleOption (StyleID, OptionType, OptionValue) VALUES
(3, 'vest', '2 piece suit'),
(3, 'vest', '3 piece suit');
UPDATE StyleOption SET Price = 74 WHERE StyleOptionID=56


-- Table: Lining
CREATE TABLE Lining (
    LiningID INT PRIMARY KEY IDENTITY(1,1),
    LiningName NVARCHAR(255),
    ImageURL NVARCHAR(255)
);

-- Table: Category
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
	CategoryParentID INT,
    Name NVARCHAR(255),
    ImageURL NVARCHAR(255),
    Description NVARCHAR(255)
    
);
-- Inserting Parent Categories
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Men', NULL);
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Women', NULL);
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Wedding', NULL);
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Accessories', NULL);
-- Inserting Child Categories for 'Men'
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Suits', (SELECT CategoryID FROM [Category] WHERE Name = 'Men'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Shirts', (SELECT CategoryID FROM [Category] WHERE Name = 'Men'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Blazers', (SELECT CategoryID FROM [Category] WHERE Name = 'Men'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Pants', (SELECT CategoryID FROM [Category] WHERE Name = 'Men'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Coat', (SELECT CategoryID FROM [Category] WHERE Name = 'Men'));
-- Inserting Child Categories for 'Women'
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Suits', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Pants', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Blazer', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Dress', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Skirt', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Top & Blouse', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Overcoat', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Business Dress', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Business Shirt', (SELECT CategoryID FROM [Category] WHERE Name = 'Women'));
-- Inserting Child Categories for 'Wedding'
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Tailored Suits for Groom and Groomsmen', (SELECT CategoryID FROM [Category] WHERE Name = 'Wedding'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Bride', (SELECT CategoryID FROM [Category] WHERE Name = 'Wedding'));
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Bridesmaid', (SELECT CategoryID FROM [Category] WHERE Name = 'Wedding'));
-- Inserting Child Categories for 'Accessories'
INSERT INTO [Category] (Name, CategoryParentID) VALUES ('Ties', (SELECT CategoryID FROM [Category] WHERE Name = 'Accessories'));


-- Table: Product
DROP TABLE IF EXISTS Product;
GO
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductCode NVARCHAR(100),
    MeasurementID INT FOREIGN KEY REFERENCES Measurement(MeasurementID),
    CategoryID INT FOREIGN KEY REFERENCES Category(CategoryID),
    FabricID INT FOREIGN KEY REFERENCES Fabric(FabricID),
    LiningID INT FOREIGN KEY REFERENCES Lining(LiningID),
	IsCustom BIT ,
	ImgURL NVARCHAR(255),
	Price DECIMAL(10,2)
);
ALTER TABLE Product
ADD SIZE VARCHAR(3)
---------------------------------------------------------------------------------------CHAY CAI NAY NE---------------------------------------
INSERT INTO Product (ProductCode, MeasurementID, CategoryID, FabricID, LiningID,IsCustom,ImgURL,Price) 
VALUES ('PRD001', 1, 2, 3, 4,1,null,650);
INSERT INTO Product (ProductCode, MeasurementID, CategoryID, FabricID, LiningID,IsCustom,ImgURL,Price) 
VALUES ('PRD002', null, 10, 5, 2,0,'https://product.hstatic.net/1000366669/product/281_10d743a5dba14ca2b79a8a8ddf2697ab_master.jpg',550);
INSERT INTO Product (ProductCode, MeasurementID, CategoryID, FabricID, LiningID,IsCustom,ImgURL,Price) 
VALUES ('PRD003', 2, 5, 9, 10,1,null,350);
INSERT INTO Product (ProductCode, MeasurementID, CategoryID, FabricID, LiningID,IsCustom,ImgURL,Price) 
VALUES ('PRD004', 2, 6, 30, 11,1,null,450);
--Table : ProductStyleOption 
DROP TABLE IF EXISTS ProductStyleOption;
GO
CREATE TABLE ProductStyleOption (
    ProductId INT NOT NULL,
    StyleOptionId INT NOT NULL,
    PRIMARY KEY (ProductId, StyleOptionId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductID),
    FOREIGN KEY (StyleOptionId) REFERENCES StyleOption(StyleOptionID)
);
INSERT INTO ProductStyleOption (ProductId, StyleOptionId) VALUES
(5, 7),  
(6, 8) 

INSERT INTO ProductStyleOption (ProductId, StyleOptionId) VALUES
(5, 1),  
(7, 4),  
(6, 7),
(6, 9),
(7, 7),
(7, 11);  
-- Table: OrderDetail
DROP TABLE IF EXISTS OrderDetail;
GO
CREATE TABLE OrderDetail (
	OrderID INT NOT NULL,
	ProductId INT NOT NULL,
	PRIMARY KEY (OrderID, ProductId),
	FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductID)
)
INSERT INTO OrderDetail (OrderID, ProductId) VALUES
(1,5),
(2,6)

--Table Feedback
DROP TABLE IF EXISTS Feedback;
GO
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    Comment NVARCHAR(255),
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    Response NVARCHAR(255),
    DateSubmitted DATE,
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    OrderID INT FOREIGN KEY REFERENCES [Order](OrderID)
);
INSERT INTO Feedback (Comment, Rating,Response,DateSubmitted,UserID,OrderID)
VALUES ('Best vest ever',5,'Thank you','2024-11-10',2,1)

INSERT INTO Lining (LiningName, ImageUrl)
VALUES 
('Silk Lining', 'https://example.com/images/silk_lining.jpg'),
('Satin Lining', 'https://example.com/images/satin_lining.jpg'),
('Polyester Lining', 'https://example.com/images/polyester_lining.jpg'),
('Viscose Lining', 'https://example.com/images/viscose_lining.jpg'),
('Cotton Lining', 'https://example.com/images/cotton_lining.jpg'),
('Rayon Lining', 'https://example.com/images/rayon_lining.jpg'),
('Acetate Lining', 'https://example.com/images/acetate_lining.jpg'),
('Bemberg Lining', 'https://example.com/images/bemberg_lining.jpg'),
('Cupro Lining', 'https://example.com/images/cupro_lining.jpg'),
('Taffeta Lining', 'https://example.com/images/taffeta_lining.jpg'),
('Jacquard Lining', 'https://example.com/images/jacquard_lining.jpg'),
('Wool Lining', 'https://example.com/images/wool_lining.jpg'),
('Flannel Lining', 'https://example.com/images/flannel_lining.jpg'),
('Velvet Lining', 'https://example.com/images/velvet_lining.jpg'),
('Linen Lining', 'https://example.com/images/linen_lining.jpg'),
('Twill Lining', 'https://example.com/images/twill_lining.jpg'),
('Mesh Lining', 'https://example.com/images/mesh_lining.jpg'),
('Stretch Lining', 'https://example.com/images/stretch_lining.jpg'),
('Fleece Lining', 'https://example.com/images/fleece_lining.jpg'),
('Microfiber Lining', 'https://example.com/images/microfiber_lining.jpg'),
('Polyamide Lining', 'https://example.com/images/polyamide_lining.jpg'),
('Chiffon Lining', 'https://example.com/images/chiffon_lining.jpg'),
('Organza Lining', 'https://example.com/images/organza_lining.jpg'),
('Batiste Lining', 'https://example.com/images/batiste_lining.jpg'),
('Georgette Lining', 'https://example.com/images/georgette_lining.jpg'),
('Brocade Lining', 'https://example.com/images/brocade_lining.jpg'),
('Net Lining', 'https://example.com/images/net_lining.jpg'),
('Gabardine Lining', 'https://example.com/images/gabardine_lining.jpg'),
('Denim Lining', 'https://example.com/images/denim_lining.jpg'),
('Canvas Lining', 'https://example.com/images/canvas_lining.jpg');

DROP TABLE IF EXISTS Inventory;
GO

CREATE TABLE ProductInventory (
    ProductID INT PRIMARY KEY,           
    Quantity INT NOT NULL,               
    LastUpdate DATETIME NOT NULL,         
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID) 
);









