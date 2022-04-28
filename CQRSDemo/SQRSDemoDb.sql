USE master
GO
CREATE DATABASE CQRSDemoDb
GO
Use CQRSDemoDb
GO
CREATE TABLE dbo.Product(
	Id int IDENTITY(1,1) Primary Key NOT NULL,
	Name varchar(100) NULL,
	UnitPrice decimal(18, 2) NOT NULL,
	Discontinued bit NOT NULL,
	Quantity decimal(18, 2) NOT NULL
)
GO