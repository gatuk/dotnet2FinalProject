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
    Password NVARCHAR(255) NOT NULL
	DEFAULT '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8',
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
([Username],[Role])
	VALUES
		('user1','Admin'),
		('user2','AirlineStaff'),
		('user3','Customer')








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
    [FlightID] [int] IDENTITY(100000, 1) PRIMARY KEY ,
    [FlightNumber] [nvarchar](10) NOT NULL,
    [Airline] [nvarchar](255) NOT NULL,
    [DepartureAirport] [nvarchar](10) NOT NULL,
    [ArrivalAirport] [nvarchar](10) NOT NULL,
    [DepartureDateTime] [DATETIME] NOT NULL,
    [ArrivalDateTime] [DATETIME] NOT NULL,
    [AvailableSeats] [int] NOT NULL DEFAULT 0,
    [Price] [Decimal] Not null default 0.0,	
	[Active] [bit] DEFAULT  1,
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


GO
print '' print '*** Creating table booking ***'
GO
CREATE TABLE Booking (
		BookingID INT IDENTITY(100000, 1) PRIMARY KEY,
		UserID INT NOT NULL,
		FlightID int NOT NULL,
		BookingDate DATETIME NOT NULL,
		TotalPrice DECIMAL(9, 2) NOT NULL,
		CONSTRAINT [fk_Booking_UserID] FOREIGN KEY ([UserID]) REFERENCES Users (UserID),
		CONSTRAINT [fk_Booking_FlightID] FOREIGN KEY ([FlightID]) REFERENCES [dbo].[Flight] ([FlightID])
)
GO
print '' print '*** inserting booking test records ***'
GO
INSERT INTO Booking
(UserID, FlightID, BookingDate, TotalPrice)
	VALUES
		(100000, 100000, '2019-12-01 00:00:00', 100.00),
		(100000, 100001, '2019-12-01 00:00:00', 100.00),
		(100000, 100002, '2019-12-01 00:00:00', 100.00),
		(100000, 100003, '2019-12-01 00:00:00', 100.00),
		(100000, 100004, '2019-12-01 00:00:00', 100.00),
		(100000, 100005, '2019-12-01 00:00:00', 100.00),
		(100000, 100006, '2019-12-01 00:00:00', 100.00),
		(100000, 100007, '2019-12-01 00:00:00', 100.00),
		(100000, 100008, '2019-12-01 00:00:00', 100.00)
GO

print '' print '*** Creating table Passenger ***'
GO
CREATE TABLE Passenger (
		PassengerID INT IDENTITY(100000, 1) PRIMARY KEY,
		FlightID int NOT NULL,
		FirstName VARCHAR(255) NOT NULL,
		LastName VARCHAR(255) NOT NULL,
		SeatNumber VARCHAR(255)  NOT NULL,
		Email VARCHAR(255) NOT NULL,
		Phone VARCHAR(255) NOT NULL,
		Address VARCHAR(255) NOT NULL,
		City VARCHAR(255) NOT NULL,
		State VARCHAR(255) NOT NULL,
		ZipCode int NOT NULL,
		IsCheckedIn bit default 1,
		IsMinor bit default 1,
		IsSpecialNeeds bit default 1,
		Active bit default 1,
		CONSTRAINT [fk_Passenger_FlightID] FOREIGN KEY ([FlightID]) REFERENCES [dbo].[Flight] ([FlightID])
);



GO
print '' print '*** inserting passenger test records ***'
GO
INSERT INTO Passenger
(FlightID,FirstName, LastName, SeatNumber,Email, Phone, Address, City, State, ZipCode)
	VALUES
		(100000,'Gish', 'Basheer','25', ' [email protected] ', '1234567899', '123 Main St', 'Iowa City', 'IA', 52404),
		(100001,'John', 'Doe','26', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', 10001),
		(100000,'p1', 'd1','27', ' [email protected] ', '1234567898', '12 Main St', 'cedar rapids', 'IA', 52401)
GO


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
	return @@ROWCOUNT
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
	return @@ROWCOUNT
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
	return @@ROWCOUNT
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
		([FlightNumber],[Airline],[DepartureAirport],[ArrivalAirport],[DepartureDateTime],[ArrivalDateTime],[AvailableSeats],[Price])
	VALUES
		(@FlightNumber, @Airline,@Departure,@Destination,@DepartureTime,@ArrivalTime, @AvailableSeats,@Price)
	return @@ROWCOUNT
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
		return @@ROWCOUNT
	END
GO
print '' print '*** Creating sp_delete_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_flight]
(@FlightId [int])
AS 	
	BEGIN
		UPDATE [dbo].[Flight] 
		SET Active = 0
		WHERE [FlightID] = @FlightId;
	return @@ROWCOUNT
	END
GO
print '' print '*** Creating sp_select_all_passengers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_passengers]
AS 	
	BEGIN
		SELECT PassengerID,FlightID,FirstName,LastName,SeatNumber,Email,Phone,Address,City,State,ZipCode,IsCheckedIn,IsMinor,IsSpecialNeeds,Active
		FROM Passenger;
	END
GO
print '' print '*** Creating sp_insert_passenger ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_passenger]
(@FlightID int,@FirstName VARCHAR(255),@LastName VARCHAR(255),@SeatNumber VARCHAR(255),@Email VARCHAR(255),@PhoneNumber VARCHAR(255),@Address VARCHAR(255),@City VARCHAR(255),@State VARCHAR(255),@ZipCode int,@IsCheckedIn bit,@IsMinor bit,@IsSpecialNeeds bit,@Active bit)
AS 	
	BEGIN
		INSERT INTO Passenger
			(FlightID,FirstName, LastName, SeatNumber,Email, Phone, Address, City, State, ZipCode,IsCheckedIn,IsMinor,IsSpecialNeeds,Active)
		Values(@FlightID,@FirstName,@LastName,@SeatNumber,@Email,@PhoneNumber,@Address,@City,@State,@ZipCode,@IsCheckedIn,@IsMinor,@IsSpecialNeeds, 			@Active)
		return @@ROWCOUNT
	END
GO
print '' print '*** Creating sp_update_passenger ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passenger]
(@PassengerID int,@FlightID int,@FirstName VARCHAR(255),@LastName VARCHAR(255),@SeatNumber VARCHAR(255),@Email VARCHAR(255),@PhoneNumber VARCHAR(255),@Address VARCHAR(255),@City VARCHAR(255),@State VARCHAR(255),@ZipCode int,@IsCheckedIn bit,@IsMinor bit,@IsSpecialNeeds bit,@Active bit)
AS 	
	BEGIN
		UPDATE Passenger
		SET	FlightID= @FlightID,
			FirstName = @FirstName,
			LastName= @LastName,
			SeatNumber= @SeatNumber,
			Email = @Email,
			Phone= @PhoneNumber,
			Address= @Address,
			City= @City,
			State= @State,
			ZipCode= @ZipCode,
			IsCheckedIn= @IsCheckedIn,
			IsMinor=@IsMinor,
			IsSpecialNeeds= @IsSpecialNeeds,
			Active= @Active
		WHERE 	PassengerID = @PassengerID
	return @@ROWCOUNT
	END
GO