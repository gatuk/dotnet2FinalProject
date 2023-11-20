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
print '' print '*** Creating sp_select_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles]
AS 	
	BEGIN
		SELECT [RoleID]
		FROM [Roles]
	END
GO
print '' print '*** Creating table users ***'
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
--new
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
print '' print '*** Creating sp_select_all_airports ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_airports]
AS 	
	BEGIN
		SELECT [AirportCode],[AirportName],[City],[Country],[Latitude],[Longitude]
		FROM [Airport]
	END
GO
print '' print '*** Creating sp_select_airport_by_code ***'
GO
CREATE PROCEDURE [dbo].[sp_select_airport_by_code]
(@AirportCode NVARCHAR(10))
AS 	
	BEGIN
		SELECT [AirportCode],[AirportName],[City],[Country],[Latitude],[Longitude]
		FROM [Airport]
		WHERE AirportCode = @AirportCode
	END
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
GO
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
print '' print '*** Creating sp_select_flight_by_number ***'
GO
CREATE PROCEDURE [dbo].[sp_select_flight_by_FlightNumber]
(@FlightNumber NVARCHAR(10))
AS 	
	BEGIN
		SELECT [FlightID],[FlightNumber],[DepartureAirport],[ArrivalAirport],[DepartureDateTime],[ArrivalDateTime],[AvailableSeats],[Price],[Airline]
		FROM [dbo].[Flight]
		WHERE FlightNumber = @FlightNumber
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
--new added
print '' print '*** Creating table aircraft ***'
CREATE TABLE Aircraft (
    AircraftID INT AUTO_INCREMENT PRIMARY KEY,
    AircraftType VARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    Airline VARCHAR(255) NOT NULL
);
GO
print '' print '*** inserting aircraft test records ***'
GO
INSERT INTO Aircraft
(AircraftType, Capacity, Airline)
	VALUES
		('Boeing 737', 100, 'American Airlines'),
		('Boeing 747', 200, 'American Airlines'),
		('Boeing 737', 100, 'Delta Airlines'),
		('Boeing 747', 200, 'Delta Airlines'),
		('Boeing 737', 100, 'United Airlines'),
		('Boeing 747', 200, 'United Airlines')
GO
print '' print '*** Creating sp_select_aircraft_by_airline ***'
GO
CREATE PROCEDURE [dbo].[sp_select_aircraft_by_airline]
(@Airline NVARCHAR(255))
AS 	
	BEGIN
		SELECT [AircraftID],[AircraftType],[Capacity],[Airline]
		FROM [dbo].[Aircraft]
		WHERE Airline = @Airline
	END
GO
print '' print '*** Creating sp_select_all_aircraft ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_aircraft]
AS 	
	BEGIN
		SELECT [AircraftID],[AircraftType],[Capacity],[Airline]
		FROM [dbo].[Aircraft]
	END
GO
print '' print '*** Creating table Passenger ***'
GO
CREATE TABLE Passenger (
		PassengerID INT AUTO_INCREMENT PRIMARY KEY,
		FirstName VARCHAR(255) NOT NULL,
		LastName VARCHAR(255) NOT NULL,
		Email VARCHAR(255) NOT NULL,
		Phone VARCHAR(255) NOT NULL,
		Address VARCHAR(255) NOT NULL,
		City VARCHAR(255) NOT NULL,
		State VARCHAR(255) NOT NULL,
		ZipCode VARCHAR(255) NOT NULL
);
GO
print '' print '*** inserting passenger test records ***'
GO
INSERT INTO Passenger
(FirstName, LastName, Email, Phone, Address, City, State, ZipCode)
	VALUES
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001'),
		('John', 'Doe', ' [email protected] ', '1234567890', '123 Main St', 'New York', 'NY', '10001')
GO
print '' print '*** Creating sp_select_passenger_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_passenger_by_id]
(@PassengerID INT)
AS 	
	BEGIN
		SELECT [PassengerID],[FirstName],[LastName],[Email],[Phone],[Address],[City],[State],[ZipCode]
		FROM [dbo].[Passenger]
		WHERE PassengerID = @PassengerID
	END
GO
print '' print '*** Creating sp_select_all_passengers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_passengers]
AS 	
	BEGIN
		SELECT [PassengerID],[FirstName],[LastName],[Email],[Phone],[Address],[City],[State],[ZipCode]
		FROM [dbo].[Passenger]
	END
GO
print '' print '*** Creating table booking ***'
GO
CREATE TABLE Booking (
		BookingID INT AUTO_INCREMENT PRIMARY KEY,
		UserID INT NOT NULL,
		FlightID INT NOT NULL,
		BookingDate DATETIME NOT NULL,
		TotalPrice DECIMAL(9, 2) NOT NULL,
		CONSTRAINT [fk_Booking_UserID] FOREIGN KEY ([UserID]) REFERENCES Users (UserID),
		CONSTRAINT [fk_Booking_FlightID] FOREIGN KEY ([FlightID]) REFERENCES Flight (FlightID)
);
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
print '' print '*** Creating sp_select_booking_by_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_booking_by_user]
(@UserID INT)
AS 	
	BEGIN
		SELECT [BookingID],[UserID],[FlightID],[BookingDate],[TotalPrice]
		FROM [dbo].[Booking]
		WHERE UserID = @UserID
	END
GO
print '' print '*** Creating sp_select_booking_by_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_select_booking_by_flight]
(@FlightID INT)
AS 	
	BEGIN
		SELECT [BookingID],[UserID],[FlightID],[BookingDate],[TotalPrice]
		FROM [dbo].[Booking]
		WHERE FlightID = @FlightID
	END
GO
print '' print '*** Creating sp_select_all_bookings ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_bookings]
AS 	
	BEGIN
		SELECT [BookingID],[UserID],[FlightID],[BookingDate],[TotalPrice]
		FROM [dbo].[Booking]
	END
GO
print '' print '*** Creating table payment ***'
GO
CREATE TABLE Payment (
		PaymentID INT AUTO_INCREMENT PRIMARY KEY,
		BookingID INT NOT NULL,
		CardNumber VARCHAR(255) NOT NULL,
		CardHolderName VARCHAR(255) NOT NULL,
		ExpiryDate DATETIME NOT NULL,
		Amount DECIMAL(9, 2) NOT NULL,
		CONSTRAINT [fk_Payment_BookingID] FOREIGN KEY ([BookingID]) REFERENCES Booking (BookingID)
);
GO
print '' print '*** inserting payment test records ***'
GO
INSERT INTO Payment
(BookingID, CardNumber, CardHolderName, ExpiryDate, Amount)
	VALUES
		(100000, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100001, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100002, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100003, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100004, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100005, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100006, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100007, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00),
		(100008, '1234567890123456', 'John Doe', '2020-12-01 00:00:00', 100.00)
GO
print '' print '*** Creating sp_select_payment_by_booking ***'
GO
CREATE PROCEDURE [dbo].[sp_select_payment_by_booking]
(@BookingID INT)
AS 	
	BEGIN
		SELECT [PaymentID],[BookingID],[CardNumber],[CardHolderName],[ExpiryDate],[Amount]
		FROM [dbo].[Payment]
		WHERE BookingID = @BookingID
	END
GO
print '' print '*** Creating sp_select_all_payments ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_payments]
AS 	
	BEGIN
		SELECT [PaymentID],[BookingID],[CardNumber],[CardHolderName],[ExpiryDate],[Amount]
		FROM [dbo].[Payment]
	END
GO
print '' print '*** Creating table ticket ***'
GO
CREATE TABLE Ticket (
		TicketID INT AUTO_INCREMENT PRIMARY KEY,
		BookingID INT NOT NULL,
		SeatNumber INT NOT NULL,
		CONSTRAINT [fk_Ticket_BookingID] FOREIGN KEY ([BookingID]) REFERENCES Booking (BookingID)
);
GO
print '' print '*** inserting ticket test records ***'
GO
INSERT INTO Ticket
(BookingID, SeatNumber)
	VALUES
		(100000, 1),
		(100001, 2),
		(100002, 3),
		(100003, 4),
		(100004, 5),
		(100005, 6),
		(100006, 7),
		(100007, 8),
		(100008, 9)
GO
print '' print '*** Creating sp_select_ticket_by_booking ***'
GO
CREATE PROCEDURE [dbo].[sp_select_ticket_by_booking]
(@BookingID INT)
AS 	
	BEGIN
		SELECT [TicketID],[BookingID],[SeatNumber]
		FROM [dbo].[Ticket]
		WHERE BookingID = @BookingID
	END
GO
print '' print '*** Creating sp_select_all_tickets ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_tickets]
AS 	
	BEGIN
		SELECT [TicketID],[BookingID],[SeatNumber]
		FROM [dbo].[Ticket]
	END
GO
print '' print '*** Creating table review ***'
GO
CREATE TABLE Review (
		ReviewID INT AUTO_INCREMENT PRIMARY KEY,
		UserID INT NOT NULL,
		FlightID INT NOT NULL,
		ReviewDate DATETIME NOT NULL,
		Rating INT NOT NULL,
		Comment VARCHAR(255) NOT NULL,
		CONSTRAINT [fk_Review_UserID] FOREIGN KEY ([UserID]) REFERENCES Users (UserID),
		CONSTRAINT [fk_Review_FlightID] FOREIGN KEY ([FlightID]) REFERENCES Flight (FlightID)
);
GO
print '' print '*** inserting review test records ***'
GO
INSERT INTO Review
(UserID, FlightID, ReviewDate, Rating, Comment)
	VALUES
		(100000, 100000, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100001, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100002, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100003, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100004, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100005, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100006, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100007, '2019-12-01 00:00:00', 5, 'Great flight!'),
		(100000, 100008, '2019-12-01 00:00:00', 5, 'Great flight!')
GO
print '' print '*** Creating sp_select_review_by_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_review_by_user]
(@UserID INT)
AS 	
	BEGIN
		SELECT [ReviewID],[UserID],[FlightID],[ReviewDate],[Rating],[Comment]
		FROM [dbo].[Review]
		WHERE UserID = @UserID
	END
GO
print '' print '*** Creating sp_select_review_by_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_select_review_by_flight]
(@FlightID INT)
AS 	
	BEGIN
		SELECT [ReviewID],[UserID],[FlightID],[ReviewDate],[Rating],[Comment]
		FROM [dbo].[Review]
		WHERE FlightID = @FlightID
	END
GO
print '' print '*** Creating sp_select_all_reviews ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_reviews]
AS 	
	BEGIN
		SELECT [ReviewID],[UserID],[FlightID],[ReviewDate],[Rating],[Comment]
		FROM [dbo].[Review]
	END
GO

print '' print '*** Creating table flight_status ***'
GO
CREATE TABLE FlightStatus (
		FlightStatusID INT AUTO_INCREMENT PRIMARY KEY,
		FlightID INT NOT NULL,
		Status VARCHAR(255) NOT NULL,
		CONSTRAINT [fk_FlightStatus_FlightID] FOREIGN KEY ([FlightID]) REFERENCES Flight (FlightID)
);
GO
print '' print '*** inserting flight_status test records ***'
GO
INSERT INTO FlightStatus
(FlightID, Status)
	VALUES
		(100000, 'On Time'),
		(100001, 'On Time'),
		(100002, 'On Time'),
		(100003, 'On Time'),
		(100004, 'On Time'),
		(100005, 'On Time'),
		(100006, 'On Time'),
		(100007, 'On Time'),
		(100008, 'On Time')
GO
print '' print '*** Creating sp_select_flight_status_by_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_select_flight_status_by_flight]
(@FlightID INT)
AS 	
	BEGIN
		SELECT [FlightStatusID],[FlightID],[Status]
		FROM [dbo].[FlightStatus]
		WHERE FlightID = @FlightID
	END
GO
print '' print '*** Creating sp_select_all_flight_status ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_flight_status]
AS 	
	BEGIN
		SELECT [FlightStatusID],[FlightID],[Status]
		FROM [dbo].[FlightStatus]
	END
GO
print '' print '*** Creating table flight_status_history ***'
GO
CREATE TABLE FlightStatusHistory (
		FlightStatusHistoryID INT AUTO_INCREMENT PRIMARY KEY,
		FlightID INT NOT NULL,
		Status VARCHAR(255) NOT NULL,
		CONSTRAINT [fk_FlightStatusHistory_FlightID] FOREIGN KEY ([FlightID]) REFERENCES Flight (FlightID)
);
GO
print '' print '*** inserting flight_status_history test records ***'
GO
INSERT INTO FlightStatusHistory
(FlightID, Status)
	VALUES
		(100000, 'On Time'),
		(100001, 'On Time'),
		(100002, 'On Time'),
		(100003, 'On Time'),
		(100004, 'On Time'),
		(100005, 'On Time'),
		(100006, 'On Time'),
		(100007, 'On Time'),
		(100008, 'On Time')
GO
print '' print '*** Creating sp_select_flight_status_history_by_flight ***'
GO
CREATE PROCEDURE [dbo].[sp_select_flight_status_history_by_flight]
(@FlightID INT)
AS 	
	BEGIN
		SELECT [FlightStatusHistoryID],[FlightID],[Status]
		FROM [dbo].[FlightStatusHistory]
		WHERE FlightID = @FlightID
	END
GO
print '' print '*** Creating sp_select_all_flight_status_history ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_flight_status_history]
AS 	
	BEGIN
		SELECT [FlightStatusHistoryID],[FlightID],[Status]
		FROM [dbo].[FlightStatusHistory]
	END
GO
-- Path: Northwind.sql
print '' print '*** Creating table Passenger ***'
GO
CREATE TABLE Passenger (
		PassengerID INT AUTO_INCREMENT PRIMARY KEY,
		BookingID INT NOT NULL,
		FirstName VARCHAR(255) NOT NULL,
		LastName VARCHAR(255) NOT NULL,
		CONSTRAINT [fk_Passenger_BookingID] FOREIGN KEY ([BookingID]) REFERENCES Booking (BookingID)
);
GO
print '' print '*** inserting Passenger test records ***'
GO
INSERT INTO Passenger
(BookingID, FirstName, LastName)
	VALUES
		(100000, 'John', 'Doe'),
		(100001, 'John', 'Doe'),
		(100002, 'John', 'Doe'),
		(100003, 'John', 'Doe'),
		(100004, 'John', 'Doe'),
		(100005, 'John', 'Doe'),
		(100006, 'John', 'Doe'),
		(100007, 'John', 'Doe'),
		(100008, 'John', 'Doe')
GO
print '' print '*** Creating sp_select_passenger_by_booking ***'
GO
CREATE PROCEDURE [dbo].[sp_select_passenger_by_booking]
(@BookingID INT)
AS 	
	BEGIN
		SELECT [PassengerID],[BookingID],[FirstName],[LastName]
		FROM [dbo].[Passenger]
		WHERE BookingID = @BookingID
	END
GO
print '' print '*** Creating sp_select_all_passengers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_passengers]
AS 	
	BEGIN
		SELECT [PassengerID],[BookingID],[FirstName],[LastName]
		FROM [dbo].[Passenger]
	END
GO
-- Path: Northwind.sql



/* EmployeeRole Table  from jim code*/
print '' print '*** creating EmployeeRole table ***'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]						NOT NULL,
	[RoleID]		[nvarchar](50)				NOT NULL,
	
	CONSTRAINT [fk_EmployeeRole_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
		
	CONSTRAINT [fk_EmployeeRole_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Roles]([RoleID]),	
		
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY([EmployeeID], [RoleID])
)
GO

print '' print '*** inserting EmployeeRole test records ***'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'Admin'),
		(100000, 'Manager'),
		(100001, 'Rental'),
		(100002, 'TourGuide'),
		(100003, 'Prep'),
		(100003, 'CheckIn'),
		(100004, 'Maintenance')
GO
print '' print '*** creating sp_select_roles_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_employeeID]
(
	@EmployeeID			[int]
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM	[EmployeeRole]
		WHERE	@EmployeeID = [EmployeeID]
	END
GO

