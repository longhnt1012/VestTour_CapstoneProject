
CREATE DATABASE VestTour;
GO

USE VestTour;
GO;
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
    IsConfirmed BIT  NOT NULL
     -- Now each user can have one role
);
INSERT INTO [User] (Name, Gender, Address, DOB, RoleID, Email, Password,IsConfirmed)
VALUES 
    ('Thanh Long', 'Nam', '111 Tran Hung Dao St', '2001-01-01', 1, 'admin123@gmail.com', '123456',1),
    ('Minh Nhut', 'Nu', 'S606B Vinhome GrandPark , Nguyen Xien St, Thu Duc city', '2002-02-15', 3, 'customer111@gmail.com', '111111',1);
---table Store
CREATE TABLE Store (
    StoreID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(255)  NOT NULL,
    Address NVARCHAR(255)  NOT NULL,
    ContactNumber NVARCHAR(15)  NOT NULL
);
---table Appointment
CREATE TABLE Appointment (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    AppointmentDate DATE  NOT NULL,
    ServiceType NVARCHAR(255)  NOT NULL,
    Status NVARCHAR(50)  NOT NULL,
    Notes NVARCHAR(255)
);
---table Booking
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    BookingDate DATE  NOT NULL,
    BookingTime TIME,
    AppointmentDetail NVARCHAR(255),
    Status NVARCHAR(50),
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    AppointmentID INT FOREIGN KEY REFERENCES Appointment(AppointmentID)
);

---table Material
CREATE TABLE Material (
    MaterialID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    MaterialName NVARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Type NVARCHAR(50),
    Brand NVARCHAR(255),
    QuantityInStock INT NOT NULL,
    Description NVARCHAR(255),
    ImageURL NVARCHAR(255),
    UnitOfMeasure NVARCHAR(50) NOT NULL
);
---table Product
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Color NVARCHAR(50) NOT NULL,
    Detail NVARCHAR(255)
);
---table TailorPartner
CREATE TABLE TailorPartner (
    PartnerID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Item NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50)
);
---table [Order]
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Deposit DECIMAL(18, 2),
    Method NVARCHAR(50),
    Status NVARCHAR(50),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    RemainingBalance DECIMAL(18, 2),
    DateOfReceive DATETIME NOT NULL,
    Quantity INT NOT NULL,
    ProductID INT FOREIGN KEY REFERENCES Product(ProductID),
    UserID INT FOREIGN KEY REFERENCES [User](UserID)
);
--table ShipperPartner
CREATE TABLE ShipperPartner (
    PartnerID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ShipperPartnerName NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    ---DeliveryTimes NVARCHAR(255),
    Status NVARCHAR(50)
);

---table Feedback
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Content NVARCHAR(255) NOT NULL,
    Rating INT,
    Response NVARCHAR(255),
    DateSubmitted DATETIME NOT NULL, ----DATETIME or DATE ?
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    OrderID INT FOREIGN KEY REFERENCES [Order](OrderID)
);
---table Payment
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	OrderID INT UNIQUE,
    Method NVARCHAR(50) NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentDetails NVARCHAR(255),
    Status NVARCHAR(50),
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID)
);
---table Inventory
CREATE TABLE Inventory (
    InventoryID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Quantity INT NOT NULL,
    ItemName NVARCHAR(255) NOT NULL,
    DateAdded DATETIME NOT NULL,
    LastUpdated DATETIME,
    Status NVARCHAR(50),
    MaterialID INT FOREIGN KEY REFERENCES Material(MaterialID),
    StoreID INT FOREIGN KEY REFERENCES Store(StoreID)
);
----Relationship between payment and order
CREATE TABLE BankAccount (
    AccountNumber VARCHAR(50) PRIMARY KEY,
    OwnerName VARCHAR(100),
    Bank VARCHAR(100),
    Content TEXT
);
ALTER TABLE Payment
ADD CONSTRAINT FK_BankAccount FOREIGN KEY (AccountNumber) REFERENCES BankAccount(AccountNumber);
CREATE TABLE PaymentHistory (
    HistoryID INT PRIMARY KEY,
    PaymentID INT,
    ChangeDate DATETIME,
    Details TEXT,
    FOREIGN KEY (PaymentID) REFERENCES Payment(PaymentID)
);
--------------------------------------------------
---table BankingAccount
CREATE TABLE BankingAccount (
    BankingAccountID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Owner] NVARCHAR(255) NOT NULL,
    Number NVARCHAR(50) NOT NULL,
    Balance DECIMAL(18, 2),
    Content NVARCHAR(255),
    Bank NVARCHAR(255),
    History NVARCHAR(255),
    UserID INT FOREIGN KEY REFERENCES [User](UserID)
);
---table Voucher
CREATE TABLE Voucher (
    VoucherID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Status NVARCHAR(50),
    VoucherCode NVARCHAR(255) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    DiscountNumber DECIMAL(18, 2),
    DateStart DATETIME,
    DateEnd DATETIME,
   --- UserID INT FOREIGN KEY REFERENCES [User](UserID)
);
