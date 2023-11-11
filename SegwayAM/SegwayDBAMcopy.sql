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
print '' print '*** creating employee table ***'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]	[int] IDENTITY(100000, 1) 	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](50)				NOT NULL,
	[Phone]			[nvarchar](11)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

print '' print '*** inserting employee test records ***'
GO
INSERT INTO [dbo].[Employee]
		([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		('Joanne', 'Smith', '3195551111', 'joanne@company.com'),
		('Martin', 'Jones', '3195552222', 'martin@company.com'),
		('Leo', 'Williams', '3195553333', 'leo@company.com'),
		('Maria', 'Perez', '3195554444', 'maria@company.com'),
		('Ahmed', 'Rawi', '3195555555', 'ahmed@company.com')
GO

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
