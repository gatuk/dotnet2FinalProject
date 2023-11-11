/* check to see if the database exists, if so, drop it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'segwaydbam')
BEGIN
	DROP DATABASE segwaydbam
	print '' print '*** dropping database segwaydbam ***'
END
GO

print '' print '*** creating database segwaydbam ***'
GO
CREATE DATABASE [segwaydbam]
GO

print '' print '*** using database segwaydbam ***'
GO
USE [segwaydbam]
GO


/* Employee Table */
-- print '' print '*** creating employee table ***'
-- GO
-- CREATE TABLE [dbo].[Flight] (
-- 	[FlightID]	[int] IDENTITY(100000, 1) 	NOT NULL,
-- 	[FlightNumber]		[nvarchar](50)				NOT NULL,
-- 	[Airline]	[nvarchar](50)				NOT NULL,
-- 	[DepartureAirport]			[nvarchar](11)				NOT NULL,
-- 	[ArrivalAirport]			[DATETIME](100)				NOT NULL,
-- 	[DepartureDateTime]	[DATETIME](100)				NOT NULL DEFAULT
-- 	[ArrivalDateTime]   [DATETIME]
-- 		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
-- 	[Active]		[bit]						NOT NULL DEFAULT 1,
-- 	CONSTRAINT [pk_FlightID] PRIMARY KEY([FlightID]),
-- 	CONSTRAINT [ak_FlightNumber] UNIQUE([FlightNumber])
-- )
-- Create the Flight table
Go
CREATE TABLE Flight (
    FlightID INT AUTO_INCREMENT PRIMARY KEY,
    FlightNumber VARCHAR(10) NOT NULL COMMENT 'Unique flight identifier',
    Airline VARCHAR(255) NOT NULL,
    DepartureAirport VARCHAR(255) NOT NULL,
    ArrivalAirport VARCHAR(255) NOT NULL,
    DepartureDateTime DATETIME NOT NULL,
    ArrivalDateTime DATETIME NOT NULL,
    AvailableSeats INT NOT NULL DEFAULT 0,
    CONSTRAINT CHK_AvailableSeats CHECK (AvailableSeats >= 0),
    INDEX idx_FlightNumber (FlightNumber),
    UNIQUE INDEX idx_FlightAirportDateTime (FlightNumber, DepartureAirport, ArrivalAirport, DepartureDateTime),
    FOREIGN KEY (DepartureAirport) REFERENCES Airport (AirportCode),
    FOREIGN KEY (ArrivalAirport) REFERENCES Airport (AirportCode)
) COMMENT 'Table for storing flight information';
Go
-- Create the Passenger table
CREATE TABLE Passenger (
    PassengerID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    EmailAddress VARCHAR(255) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15),
    INDEX idx_Name (LastName, FirstName),
    INDEX idx_DateOfBirth (DateOfBirth)
) COMMENT 'Table for storing passenger information';
go

-- Create the Booking table
CREATE TABLE Booking (
    BookingID INT AUTO_INCREMENT PRIMARY KEY,
    FlightID INT NOT NULL,
    PassengerID INT NOT NULL,
    SeatNumber VARCHAR(10),
    BookingDateTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    BookingStatus ENUM('Confirmed', 'Pending', 'Cancelled') DEFAULT 'Confirmed',
    INDEX idx_FlightPassenger (FlightID, PassengerID),
    FOREIGN KEY (FlightID) REFERENCES Flight (FlightID),
    FOREIGN KEY (PassengerID) REFERENCES Passenger (PassengerID)
) COMMENT 'Table for storing booking information';
go

CREATE TABLE Airport (
    AirportCode VARCHAR(10) PRIMARY KEY,
    AirportName VARCHAR(255) NOT NULL,
    City VARCHAR(255) NOT NULL,
    Country VARCHAR(255) NOT NULL,
    Latitude DECIMAL(9, 6) NOT NULL,
    Longitude DECIMAL(9, 6) NOT NULL
) COMMENT 'Table for storing airport information';

-- Create the Aircraft table
CREATE TABLE Aircraft (
    AircraftID INT AUTO_INCREMENT PRIMARY KEY,
    AircraftType VARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    Airline VARCHAR(255) NOT NULL,
    INDEX idx_Airline (Airline)
) COMMENT 'Table for storing aircraft information';


-- Create the User table
CREATE TABLE User (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'AirlineStaff', 'Customer') NOT NULL DEFAULT 'Customer',
    INDEX idx_Role (Role)
) 

CREATE PROCEDURE BookFlight(
    IN pFlightNumber VARCHAR(10),
    IN pFirstName VARCHAR(50),
    IN pLastName VARCHAR(50),
    IN pDateOfBirth DATE,
    IN pEmailAddress VARCHAR(255),
    IN pPhoneNumber VARCHAR(15),
    OUT pBookingStatus VARCHAR(20)
)
BEGIN
    DECLARE vFlightID INT;
    DECLARE vPassengerID INT;
    DECLARE vAvailableSeats INT;
    
    -- Check if the flight exists
    SELECT FlightID, AvailableSeats
    INTO vFlightID, vAvailableSeats
    FROM Flight
    WHERE FlightNumber = pFlightNumber;
    
    IF vFlightID IS NULL THEN
        SET pBookingStatus = 'Flight not found';
    ELSE
        -- Check if there are available seats
        IF vAvailableSeats > 0 THEN
            -- Insert the passenger information if not already in the Passenger table
            INSERT IGNORE INTO Passenger (FirstName, LastName, DateOfBirth, EmailAddress, PhoneNumber)
            VALUES (pFirstName, pLastName, pDateOfBirth, pEmailAddress, pPhoneNumber);
            
            -- Retrieve the PassengerID
            SELECT PassengerID INTO vPassengerID
            FROM Passenger
            WHERE FirstName = pFirstName AND LastName = pLastName
            AND DateOfBirth = pDateOfBirth AND EmailAddress = pEmailAddress;
            
            -- Insert a booking record
            INSERT INTO Booking (FlightID, PassengerID, SeatNumber, BookingDateTime, BookingStatus)
            VALUES (vFlightID, vPassengerID, NULL, NOW(), 'Confirmed');
            
            -- Decrement the available seats
            UPDATE Flight
            SET AvailableSeats = AvailableSeats - 1
            WHERE FlightID = vFlightID;
            
            SET pBookingStatus = 'Booking successful';
        ELSE
            SET pBookingStatus = 'No available seats';
        END IF;
    END IF;
    
END //








GO

print '' print '*** inserting employee test records ***'
GO
INSERT INTO [dbo].[Passenger]
([PassengerID],[FirstName], [LastName], [DateOfBirth], [EmailAddress], 
[PhoneNumber])
	VALUES
		(1, 'Joanne', 'Smith', 'date of birth', 'joanne@company.com', '3195551111'),
		(2, 'Martin', 'Jones', 'date of birth', 'joanne@company.com','3195551111')
		(3, 'Joanne', 'Smith', 'date of birth', 'joanne@company.com', '3195551111'),
		(4, 'Martin', 'Jones', 'date of birth', 'joanne@company.com','3195551111')
		(5, 'Joanne', 'Smith', 'date of birth', 'joanne@company.com', '3195551111'),
		(6, 'Martin', 'Jones', 'date of birth', 'joanne@company.com','3195551111')
	




/* Role Table */
print '' print '*** creating role table ***'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar](50)
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO

print '' print '*** inserting role test records ***'
GO
INSERT INTO [dbo].[Role]
		([RoleID])
	VALUES
		('Rental'),
		('TourGuide'),
		('CheckIn'),
		('Maintenance'),
		('Prep'),
		('Manager'),
		('Admin')
GO

/* EmployeeRole Table */
print '' print '*** creating EmployeeRole table ***'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]						NOT NULL,
	[RoleID]		[nvarchar](50)				NOT NULL,
	
	CONSTRAINT [fk_EmployeeRole_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
		
	CONSTRAINT [fk_EmployeeRole_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID]),	
		
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

/* login related stored procedures */

print '' print '*** creating sp_authenticate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_employee]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		SELECT 	COUNT([EmployeeID]) as 'Authenticated'
		FROM	[Employee]
		WHERE	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	[Active] = 1
	END
GO

print '' print '*** creating sp_select_employee_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
(
	@Email			[nvarchar](100)
)
AS
	BEGIN
		SELECT 	[EmployeeID], [GivenName], [FamilyName], [Phone],
					[Email], [Active]
		FROM	[Employee]
		WHERE	@Email = [Email]
	END
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

print '' print '*** creating sp_update_passwordHash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@Email				[nvarchar](100),
	@NewPasswordHash	[nvarchar](100),
	@OldPasswordHash  	[nvarchar](100)
)
AS
	BEGIN
		UPDATE 	[Employee]
		SET		[PasswordHash] = @NewPasswordHash
		WHERE	@Email = [Email]
		  AND	@OldPasswordHash = [PasswordHash]
		RETURN 	@@ROWCOUNT
	END
GO

/* Other Tables */

print '' print '*** creating Customer table ***'
GO
CREATE TABLE [dbo].[Customer] (
	[CustomerID]	[int] IDENTITY(100000, 1) 	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](50)				NOT NULL,
	[Phone]			[nvarchar](11)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PhotoID]		[nvarchar](50)				NOT NULL,
	CONSTRAINT [pk_Customer] PRIMARY KEY([CustomerID])
)
GO

print '' print '*** creating TourGuide table ***'
GO

print '' print '*** creating prep table ***'
GO

print '' print '*** creating Maintenance table ***'
GO

print '' print '*** creating Mline table ***'
GO

print '' print '*** creating CheckIn table ***'
GO






print '' print '*** creating Type table ***'
GO
CREATE TABLE [dbo].[SegwayType] (
	[TypeID]		[nvarchar](25),
	[PricePerHour]   [money],
	[MaxWeight]		[int],
	CONSTRAINT [pk_Type] PRIMARY KEY([TypeID])
)
GO

print '' print '*** creating Status table ***'
GO
CREATE TABLE [dbo].[Status] (
	[StatusID]		[nvarchar](25),
	CONSTRAINT [pk_Status] PRIMARY KEY([StatusID])
)
GO

print '' print '*** creating Segway table ***'
GO
CREATE TABLE [dbo].[Segway] (
	[SegwayID]		[nvarchar](25),
	[Color]			[nvarchar](25),
	[Name]			[nvarchar](25),
	[TypeID]		[nvarchar](25),
	[StatusID]		[nvarchar](25),
	
	[Active]	[bit]     DEFAULT 1,
	CONSTRAINT [pk_Segway] PRIMARY KEY([SegwayID]),
	CONSTRAINT [fk_Segway_TypeID] FOREIGN KEY([TypeID])
		REFERENCES [dbo].[SegwayType]([TypeID]),
	CONSTRAINT [fk_Segway_StatusID] FOREIGN KEY([StatusID])
		REFERENCES [dbo].[Status]([StatusID])
)
GO



print '' print '*** inserting type test records'
GO
INSERT INTO [dbo].[SegwayType]
			([TypeID],[PricePerHour],[MaxWeight])
			VALUES
			('Small',5.0, 100),
			('Medium',10.0, 150),
			('Large',15.0, 225),
			('Extra',20.0, 300)
			
GO			

print '' print '*** inserting Status test records'
GO
INSERT INTO [dbo].[Status]
			([StatusID])
			VALUES
			('For Rent'),
			('Awaiting Tour'),
			('Out'),
			('Needs Prep'),
			('Needs Maintenance')
Go

print '' print '*** inserting Segway test records'
GO
INSERT INTO [dbo].[Segway]
			([SegwayID],[Color],[Name],[TypeID],[StatusID])
			VALUES
			('AAAAA','Apricot','Andy','Small','For Rent'),
			('BBBBB','Blue','Barty','Small','For Rent'),
			('CCCCC','Red','Ahmed','Small','For Rent'),
			('DDDDD','WHILE','Adam','Medium','For Rent'),
			('EEEEE','Yellow','Mohamed','Medium','For Rent'),
			('FFFFF','Gray','Taha','Medium','For Rent'),
			('GGGGG','Black','Moe','Large','For Rent'),
			('HHHHH','Earth','Maria','Large','For Rent'),
			('IIIII','Iris','Mihn','Large','For Rent'),
			('JJJJJ','Khaki','Ali','Extra','For Rent'),
			('KKKKK','Lemon','Doe','Extra','For Rent'),
			('LLLLL','Gold','Jane','Extra','For Rent')		
GO
			
/* Other Table*/

print '' print '*** Creating Select_Segway_By_StatusID ***'
GO
CREATE PROCEDURE [dbo].[Select_Segways_By_StatusID]
(
	@StatusID 			[nvarchar](25)
)
AS 	
		BEGIN
			SELECT [SegwayID], [Color], [Name], [Segway].[TypeID], [StatusID],
			[Active], [PricePerHour], [MaxWeight]
			FROM [Segway] JOIN [SegwayType] on [Segway].[TypeID] = [SegwayType].[TypeID]
			WHERE @StatusID = [StatusID]
				AND ACTIVE = 1
			END
GO
			
print '' print '*** Creating Select_Segway_By_ID ***'
GO
CREATE PROCEDURE [dbo].[Select_Segway_By_ID]
(
	@SegwayID 			[nvarchar](25)
)
AS 	
		BEGIN
			SELECT [SegwayID], [Color], [Name], [Segway].[TypeID], [StatusID],
			[Active], [PricePerHour], [MaxWeight]
			FROM [Segway] JOIN [SegwayType] on [Segway].[TypeID] = [SegwayType].[TypeID]
			WHERE @SegwayID = [SegwayID]
				AND ACTIVE = 1
			END
GO	

print '' print '*** Creating Select_all_Status ***'
GO
CREATE PROCEDURE [dbo].[Select_all_Status]

AS 	
		BEGIN
			SELECT [StatusID]
			FROM [Status]
			END
GO	

print '' print '*** Creating Select_all_Type ***'
GO
CREATE PROCEDURE [dbo].[Select_all_Type]

AS 	
		BEGIN
			SELECT [TypeID]
			FROM [Type]
			END
GO	

print '' print '*** Creating Deactivate_Segway_By_ID ***'
GO
CREATE PROCEDURE [dbo].[Deactivate_Segway_By_ID]
(
	@SegwayID  [nvarchar](25)
)
AS 	
		BEGIN
			UPDATE [Segway]
			SET [Active] = 0
			WHERE @SegwayID = [SegwayID]
		END
GO	


print '' print '*** creating Rental table ***'
GO
CREATE TABLE [dbo].[Rental] (
	[RentalID]		[int] IDENTITY(100000, 1) 	NOT NULL,	
	[EmployeeID]	[int]					 	NOT NULL,
	[CustomerID]	[int]						NOT NULL,
	[DateTime]		[datetime]					NOT NULL,
	CONSTRAINT [pk_Rental] PRIMARY KEY([RentalID]),
	CONSTRAINT [fk_Rental_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	CONSTRAINT [fk_Rental_CustomerID] FOREIGN KEY([CustomerID])
		REFERENCES [dbo].[Customer]([CustomerID])	
)
GO

print '' print '*** creating Rline table ***'
GO
CREATE TABLE [dbo].[Rline] (
	[RentalID]		[int]					 	NOT NULL,	
	[SegwayID]		[nvarchar](25)			 	NOT NULL,
	[PricePerHour]	[float]						NOT NULL,
	[NumberOfHours]	[float]						NOT NULL,
	[LateCharge]	[float]						NOT NULL,
	[DamageChare]	[float]						NOT NULL,
	CONSTRAINT [fk_Rline_RentalID] FOREIGN KEY([RentalID])
		REFERENCES [dbo].[Rental]([RentalID]),
	CONSTRAINT [fk_Rline_SegwayID] FOREIGN KEY([SegwayID])
		REFERENCES [dbo].[Segway]([SegwayID]),
	CONSTRAINT [pk_Rline] PRIMARY KEY([RentalID], [SegwayID])
)
GO
