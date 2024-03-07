-- Scaffolding script
-------------------------------------------------
scaffold-dbcontext "Data Source=5400-TI11989\MSSQLSERVER01;Initial 
Catalog=CreditScore;Trusted_Connection=True;Integrated Security=True;
TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models



-- Table scripts
-------------------------------------------------
use CreditScore;
 
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CONSTRAINT UC_Users_Username UNIQUE (Username),
    CONSTRAINT UC_Users_Email UNIQUE (Email),
    CreatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET(),
    UpdatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET()
);
select * from Users;
 
CREATE TABLE Tokens (
    TokenID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    TokenValue NVARCHAR(255) NOT NULL,
    ExpiryDateTime DATETIME NOT NULL,
    CreatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET(),
    UpdatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET()
);
select * from Tokens;
 
CREATE TABLE CreditScores (
    CreditId INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    CreditScore INT NOT NULL,
    DebtToIncomeRatio DECIMAL(5,2),
    CreatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET(),
    UpdatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET()
);
select * from CreditScores;
 
CREATE TABLE FinancialDetails (
    Fid INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Income DECIMAL(15,2),
    Expenses DECIMAL(15,2),
    OtherFinancialInfo NVARCHAR(MAX),
    CreatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET(),
    UpdatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET()
);
select * from FinancialDetails;
 
CREATE TABLE Documents (
    DocumentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    FileName NVARCHAR(255),
    FilePath NVARCHAR(MAX),
    CreatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET(),
    UpdatedDate DateTimeOffset NOT NULL default SYSDATETIMEOFFSET()
);
select * from Documents;
 
CREATE TABLE Notifications (
    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
    NotificationName NVARCHAR(255),
    NoteStatus varchar(100),
    NotificationDate DATETIME DEFAULT GETDATE(),
    UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId) 
);
select * from Notifications;
 
CREATE TABLE AuditTrail (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    ActivityDescription NVARCHAR(MAX),
    Timestamp DATETIME DEFAULT GETDATE(),
   TableName NVARCHAR(MAX)
);
select * from AuditTrail;
