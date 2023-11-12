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
    Password NVARCHAR(255) NOT NULL,[Role] [ENUM('Admin', 'AirlineStaff', 'Customer')] NOT NULL DEFAULT 'Customer',
		CONSTRAINT [pk_UserID] PRIMARY KEY([UserID]),
		INDEX idx_Role (Role)
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
--create table, inset into table, create stored procedure
-- Path: Northwind.sql	

CREATE TABLE  [dbo].[Flight] (
    [FlightID] [int] IDENTITY(100000, 1) 	NOT NULL,
    [FlightNumber] [nvarchar](10) NOT NULL COMMENT 'Unique flight identifier',
    [Airline] [nvarchar](255) NOT NULL,
    [DepartureAirport] [nvarchar](255) NOT NULL,
    [ArrivalAirport] [nvarchar](255) NOT NULL,
    [DepartureDateTime] [DATETIME] NOT NULL,
    [ArrivalDateTime] [DATETIME] NOT NULL,
    [AvailableSeats] [int] NOT NULL DEFAULT 0,
    CONSTRAint CHK_AvailableSeats CHECK (AvailableSeats >= 0),
    INDEX idx_FlightNumber (FlightNumber),
    UNIQUE INDEX idx_FlightAirportDateTime (FlightNumber, DepartureAirport, ArrivalAirport, DepartureDateTime),
    FOREIGN KEY (DepartureAirport) REFERENCES Airport (AirportCode),
    FOREIGN KEY (ArrivalAirport) REFERENCES Airport (AirportCode)
) COMMENT 'Table for storing flight information';
Go
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
print '' print '*** Creating sp_verify_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_flight]
(
	@flightNumber NVARCHAR(10),
	@departureAirport NVARCHAR(255),
	@arrivalAirport NVARCHAR(255),
	@departureDateTime DATETIME
)	
AS
	BEGIN
		SELECT [FlightID]
		FROM [Flight]
		WHERE
			FlightNumber = @flightNumber
		AND DepartureAirport = @departureAirport
		AND ArrivalAirport = @arrivalAirport
		AND DepartureDateTime = @departureDateTime
	END
GO
-- Create the Passenger table
CREATE TABLE [dbo].[Passenger] (
    [PassengerID] [int] IDENTITY(100000, 1) 	NOT NULL,
    [FirstName] [nvarchar](50) NOT NULL,
    [LastName] [nvarchar](50) NOT NULL,
    [DateOfBirth] [DATE] NOT NULL,
    [EmailAddress] [nvarchar](255) NOT NULL UNIQUE,
    [PhoneNumber] [nvarchar](15),
    INDEX idx_Name (LastName, FirstName),
    INDEX idx_DateOfBirth (DateOfBirth)
) COMMENT 'Table for storing passenger information';
go
print '' print '*** inserting passenger test records ***'
GO
INSERT INTO [dbo].[Passenger]
([FirstName],[LastName],[DateOfBirth],[EmailAddress],[PhoneNumber])
	VALUES('John', 'Smith', '1990-01-01', 'abcd@gmail.com'),
	('Jane', 'Smith', '1990-01-01', 'abcd@gmail.com')

GO
print '' print '*** Creating sp_verify_passenger ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_passenger]
(
	@firstName NVARCHAR(50),
	@lastName NVARCHAR(50),
	@dateOfBirth DATE,
	@emailAddress NVARCHAR(255)
)
AS
	BEGIN
		SELECT [PassengerID]
		FROM [Passenger]
		WHERE
			FirstName = @firstName
		AND LastName = @lastName
		AND DateOfBirth = @dateOfBirth
		AND EmailAddress = @emailAddress
	END
GO
-- Create the Booking table
CREATE TABLE [dbo].[Booking] (
    [BookingID] [int] IDENTITY(100000, 1) 	NOT NULL,
    [FlightID] [int] NOT NULL,
    [PassengerID] [int] NOT NULL,
    [SeatNumber] [nvarchar](10),
    [BookingDateTime] [DATETIME] DEFAULT CURRENT_TIMESTAMP,
    [BookingStatus] ENUM('Confirmed', 'Pending', 'Cancelled') DEFAULT 'Confirmed',
    INDEX idx_FlightPassenger (FlightID, PassengerID),
    FOREIGN KEY (FlightID) REFERENCES Flight (FlightID),
    FOREIGN KEY (PassengerID) REFERENCES Passenger (PassengerID)
) COMMENT 'Table for storing booking information';
go
print '' print '*** inserting booking test records ***'
GO
INSERT INTO [dbo].[Booking]
([FlightID],[PassengerID],[SeatNumber])
	VALUES
		(100000, 100000, '1A'),
		(100000, 100001, '1B')
GO
print '' print '*** Creating sp_verify_booking ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_booking]
(
	@flightID INT,
	@passengerID INT
)
AS
	BEGIN
		SELECT [BookingID]
		FROM [Booking]
		WHERE
			FlightID = @flightID
		AND PassengerID = @passengerID
	END
GO
CREATE TABLE [dbo].[Airport] (
    [AirportCode] [nvarchar](10) PRIMARY KEY,
    [AirportName] [nvarchar](255) NOT NULL,
    [City] [nvarchar](255) NOT NULL,
    [Country] [nvarchar](255) NOT NULL,
    [Latitude] [DECIMAL](9, 6) NOT NULL,
    [Longitude] [DECIMAL](9, 6) NOT NULL
) COMMENT 'Table for storing airport information';
go
print '' print '*** inserting airport test records ***'
GO
INSERT INTO [dbo].[Airport]
([AirportCode],[AirportName],[City],[Country],[Latitude],[Longitude])
	VALUES
		('JFK', 'John F. Kennedy International Airport', 'New York', 'United States', 40.6413, -73.7781),
		('LAX', 'Los Angeles International Airport', 'Los Angeles', 'United States', 33.9416, -118.4085)
GO
print '' print '*** Creating sp_verify_airport ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_airport]
(
	@airportCode NVARCHAR(10)
)
AS
	BEGIN
		SELECT [AirportCode]
		FROM [Airport]
		WHERE
			AirportCode = @airportCode
	END
GO

-- Create the Aircraft table
CREATE TABLE [dbo].[Aircraft] (
	--ask aircraftid string or integer
    [AircraftID] [int] AUTO_INCREMENT PRIMARY KEY,
    [AircraftType] [nvarchar](255) NOT NULL,
    [Capacity] [int] NOT NULL,
    [Airline] [nvarchar](255) NOT NULL,
    INDEX idx_Airline (Airline)
) COMMENT 'Table for storing aircraft information';
go
print '' print '*** inserting aircraft test records ***'
GO
INSERT INTO [dbo].[Aircraft]
([AircraftType],[Capacity],[Airline])
	VALUES
		('Boeing 737', 100, 'American Airlines'),
		('Boeing 747', 200, 'American Airlines'),
		('Boeing 737', 100, 'Delta Airlines'),
		('Boeing 747', 200, 'Delta Airlines')
GO
print '' print '*** Creating sp_verify_aircraft ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_aircraft]
(
	@aircraftID INT
)
AS
	BEGIN
		SELECT [AircraftID]
		FROM [Aircraft]
		WHERE
			AircraftID = @aircraftID
	END
GO
-- Create the User table
CREATE TABLE [dbo].[User] (
    [UserID] [int] IDENTITY(100000, 1) 	NOT NULL,
    [Username] [nvarchar](50) NOT NULL UNIQUE,
    [Password] [nvarchar](255) NOT NULL,
    [Role] [ENUM('Admin', 'AirlineStaff', 'Customer')] NOT NULL DEFAULT 'Customer',
    INDEX idx_Role (Role)
) 
GO
print '' print '*** inserting user test records ***'
GO

INSERT INTO [dbo].[User]
([Username],[Password],[Role])
  VALUES
    ('admin', '<PASSWORD>', 'Admin'),
		('airline_staff', '<PASSWORD>', 'AirlineStaff'),
		('customer', '<PASSWORD>', 'Customer')
GO
print '' print '*** Creating sp_verify_user ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_user]
(
	@username NVARCHAR(50),
	@password NVARCHAR(255)
)
  BEGIN
    SELECT [UserID]
    FROM [User]
    WHERE
			Username = @username
		AND	Password = @password
			END
GO

--create role table
CREATE TABLE [dbo].[Role] (
		[RoleID] [int] IDENTITY(100000, 1) 	NOT NULL,
		[RoleName] [nvarchar](50) NOT NULL UNIQUE,
		[Description] [nvarchar](255) NOT NULL,
		INDEX idx_RoleName (RoleName)
)
GO
print '' print '*** inserting role test records ***'
GO
INSERT INTO [dbo].[Role]
([RoleName],[Description])
	VALUES
		('Admin', 'Admin'),
		('AirlineStaff', 'Airline Staff'),
		('Customer', 'Customer')
GO
print '' print '*** Creating sp_verify_role ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_role]
(
	@roleName NVARCHAR(50)
)
AS
	BEGIN
		SELECT [RoleID]
		FROM [Role]
		WHERE
			RoleName = @roleName
	END
GO

--create ZipCode table
CREATE TABLE [dbo].[ZipCode] (
		[ZipCode] [int] IDENTITY(100000, 1) 	NOT NULL,
		[City] [nvarchar](50) NOT NULL,
		[State] [nvarchar](50) NOT NULL,
		[Latitude] [DECIMAL](9, 6) NOT NULL,
		[Longitude] [DECIMAL](9, 6) NOT NULL,
		INDEX idx_ZipCode (ZipCode)
)
GO
print '' print '*** inserting zipcode test records ***'
GO
INSERT INTO [dbo].[ZipCode]
([City],[State],[Latitude],[Longitude])
	VALUES
		('New York', 'NY', 40.7128, -74.0060),
		('Los Angeles', 'CA', 34.0522, -118.2437)
GO
print '' print '*** Creating sp_verify_zipcode ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_zipcode]
(
	@zipCode INT
)
AS
	BEGIN
		SELECT [ZipCode]
		FROM [ZipCode]
		WHERE
			ZipCode = @zipCode
	END	
GO








