/* check to see if the database exists, if so, drop it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'Northwind')
BEGIN
	DROP DATABASE Northwind
	print '' print '*** dropping database Northwind ***'
END
GO

print '' print '*** creating database Northwind ***'
GO
CREATE DATABASE [Northwind]
GO

print '' print '*** using database Northwind ***'
GO
USE [Northwind]
GO
CREATE TABLE Users (
    UserID INT IDENTITY(100000, 1) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    CONSTRAINT [pk_UserID] PRIMARY KEY([UserID])
) 
GO
print '' print '*** inserting user test records ***'
GO
INSERT INTO [dbo].[Users]
([Username],[Password])
	VALUES
		('user1', 'password'),
		('user2', 'password')
GO
print '' print '*** Creating sp_verify_user ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_user]
(
	@username NVARCHAR(50),
	@password NVARCHAR(255)
)

AS 	
		BEGIN
			SELECT [UserID]
			FROM [Users]
			WHERE
				Username = @username
			AND	Password = @password
		END
GO	