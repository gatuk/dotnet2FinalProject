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
CREATE TABLE Roles (
    RoleID NVARCHAR(50) NOT NULL,
    Description NVARCHAR(50) NOT NULL,
   CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID]),
)
GO
print '' print '*** inserting Roles test records ***'
GO
INSERT INTO [dbo].[Roles]
([RoleID],[Description])
	VALUES
		('Admin', 'This is admin role'),
		('AirlineStaff', 'This is AirlineStaff role'),
		('Customer','This is Customer Role')
GO
CREATE TABLE Users (
    UserID INT IDENTITY(100000, 1) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    Active bit default 1,
  CONSTRAINT [pk_UserID] PRIMARY KEY([UserID]),
  CONSTRAINT [fk_Role] FOREIGN KEY([Role])
		REFERENCES [dbo].[Roles]([RoleID]),	
)
GO
print '' print '*** inserting user test records ***'
GO
INSERT INTO [dbo].[Users]
([Username],[Password],[Role])
	VALUES
		('user1', 'password','Admin'),
		('user2', 'password','AirlineStaff'),
		('user3', 'password','Customer')
GO
print '' print '*** Creating sp_verify_user ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_user]
(@username NVARCHAR(50),@password NVARCHAR(255))
AS 	
	BEGIN
		SELECT [Role]
		FROM [Users]
		WHERE	Username = @username
		AND	Password = @password
	END
GO
print '' print '*** Creating table Airport ***'
GO
CREATE TABLE [dbo].[Airport] (
    [AirportCode] [nvarchar](10) PRIMARY KEY,
    [AirportName] [nvarchar](255) NOT NULL,
    [City] [nvarchar](255) NOT NULL,
    [Country] [nvarchar](255) NOT NULL,
    [Latitude] [DECIMAL](9, 6) NOT NULL,
    [Longitude] [DECIMAL](9, 6) NOT NULL
) 
GO
print '' print '*** inserting Airport test records ***'
GO
INSERT INTO [dbo].[Airport]
([AirportCode],[AirportName],[City],[Country],[Latitude],[Longitude])
	VALUES
		('JFK', 'Johon Kennedy','New York','United States',40.6413, -73.7781),
		('LAX', 'Los Angeles International Airport', 'Los Angeles', 'United States', 33.9416, -118.4085)
GO
GO
print '' print '***create table Flight ***'	

CREATE TABLE  [dbo].[Flight] (
    [FlightID] [int] IDENTITY(100000, 1) 	NOT NULL,
    [FlightNumber] [nvarchar](10) NOT NULL,
    [Airline] [nvarchar](255) NOT NULL,
    [DepartureAirport] [nvarchar](10) NOT NULL,
    [ArrivalAirport] [nvarchar](10) NOT NULL,
    [DepartureDateTime] [DATETIME] NOT NULL,
    [ArrivalDateTime] [DATETIME] NOT NULL,
    [AvailableSeats] [int] NOT NULL DEFAULT 0,
    [Price] [Decimal] Not null default 0.0,	
    CONSTRAINT CHK_AvailableSeats CHECK (AvailableSeats >= 0),
    CONSTRAINT [fk_DepartureAirport] FOREIGN KEY ([DepartureAirport]) REFERENCES Airport (AirportCode),
    CONSTRAINT [fk_ArrivalAirport] FOREIGN KEY ([ArrivalAirport]) REFERENCES Airport (AirportCode)
)
print '' print '*** Creating index idx_FlightNumber ***'
GO
CREATE INDEX idx_FlightNumber
ON [dbo].[Flight] ( FlightNumber, DepartureAirport, ArrivalAirport, DepartureDateTime);
GO
print '' print '*** inserting flight test records ***'	
GO
INSERT INTO [dbo].[Flight]
([FlightNumber],[Airline],[DepartureAirport],[ArrivalAirport],[DepartureDateTime],[ArrivalDateTime],[AvailableSeats])
	VALUES
		('AA100', 'American Airlines', 'JFK', 'LAX', '2019-12-01 08:00:00', '2019-12-01 11:00:00', 100),
		('AA101', 'American Airlines', 'JFK', 'LAX', '2019-12-01 10:00:00', '2019-12-01 13:00:00', 100),
		('AA102', 'American Airlines', 'JFK', 'LAX', '2019-12-01 12:00:00', '2019-12-01 15:00:00', 100),
		('AA103', 'American Airlines', 'JFK', 'LAX', '2019-12-01 14:00:00', '2019-12-01 17:00:00', 100),
		('AA104', 'American Airlines', 'JFK', 'LAX', '2019-12-01 16:00:00', '2019-12-01 19:00:00', 100),
		('AA105', 'American Airlines', 'JFK', 'LAX', '2019-12-01 18:00:00', '2019-12-01 21:00:00', 100),
		('AA106', 'American Airlines', 'JFK', 'LAX', '2019-12-01 20:00:00', '2019-12-01 23:00:00', 100),
		('AA107', 'American Airlines', 'JFK', 'LAX', '2019-12-01 22:00:00', '2019-12-02 01:00:00', 100),
		('AA108', 'American Airlines', 'JFK', 'LAX', '2019-12-02 00:00:00', '2019-12-02 03:00:00', 100)
GO
print '' print '*** Creating sp_select_users ***'
GO
CREATE PROCEDURE [dbo].[sp_select_users]
AS 	
	BEGIN
		SELECT [UserID],[Username],[Password],[Role]
		FROM [Users]
		WHERE Active = 1
	END
GO
print '' print '*** Creating sp_select_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles]
AS 	
	BEGIN
		SELECT [RoleID]
		FROM [Roles]
	END
GO

print '' print '*** Creating sp_insert_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user]
(@username NVARCHAR(50),@Password NVARCHAR(255),@Role NVARCHAR(50))
AS 	
	BEGIN
		INSERT INTO [dbo].[Users]
		([Username],[Password],[Role])
	VALUES
		(@username, @Password,@Role)
	END
GO
print '' print '*** Creating sp_update_user ***'
GO
CREATE PROCEDURE [dbo].[sp_update_user]
(@UserId int,@username NVARCHAR(50),@Password NVARCHAR(255),@Role NVARCHAR(50))
AS 	
	BEGIN
		UPDATE [dbo].[Users]
		SET [Username] = @username,[Password]= @Password,[Role] = @Role
		WHERE UserID=@UserId;
	END
GO
print '' print '*** Creating sp_delete_user ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user]
(@UserId int)
AS 	
	BEGIN
		UPDATE [dbo].[Users]
		SET [Active] = 0
		WHERE UserID=@UserId;
	END
GO
print '' print '*** Creating sp_select_all_flights ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_flights]
AS 	
	BEGIN
		SELECT FlightID,[FlightNumber],DepartureAirport,ArrivalAirport,DepartureDateTime, ArrivalDateTime,AvailableSeats,Price,Airline
		FROM [dbo].[Flight]
	END
GO
print '' print '*** Creating sp_insert_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_flight]
(@FlightNumber [nvarchar](10),@Departure [nvarchar](10),@Destination[nvarchar](10),
@DepartureTime  [DATETIME], @ArrivalTime  [DATETIME], @AvailableSeats [int], @Price [Decimal], @Airline [nvarchar](255))
AS 	
	BEGIN
		INSERT INTO [dbo].[Flight]
		([FlightNumber],[Airline],[DepartureAirport],[ArrivalAirport],[DepartureDateTime],[ArrivalDateTime],[AvailableSeats])
	VALUES
		(@FlightNumber, @Airline,@Departure,@Destination,@DepartureTime,@ArrivalTime, @AvailableSeats)
	END
GO
print '' print '*** Creating sp_select_all_airport_codes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_airport_codes]
AS 	
	BEGIN
		SELECT [AirportCode]
		FROM [dbo].[Airport]
	END
GO
print '' print '*** Creating sp_update_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_update_flight]
(@FlightId [int], @FlightNumber [nvarchar](10),@Departure [nvarchar](10),@Destination[nvarchar](10),
@DepartureTime  [DATETIME], @ArrivalTime  [DATETIME], @AvailableSeats [int], @Price [Decimal], @Airline [nvarchar](255))
AS 	
	BEGIN
		UPDATE [dbo].[Flight]
		SET	[FlightNumber]= @FlightNumber,
		   	[Airline]= @Airline,
			[DepartureAirport]= @Departure,
			[ArrivalAirport]=  @Destination,
			[DepartureDateTime]= @DepartureTime,
			[ArrivalDateTime]= @ArrivalTime,
			[AvailableSeats] = @AvailableSeats,
			[Price]	= @Price
		WHERE	[FlightID] = @FlightId
		
	END
GO
