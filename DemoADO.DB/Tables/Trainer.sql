CREATE TABLE [dbo].[Trainer]
(
	[Id] INT IDENTITY,
	[FirstName] NVARCHAR(55) NOT NULL,
	[LastName] NVARCHAR(55) NOT NULL,
	[BirthDate] DATE,
	[IsActive] BIT DEFAULT 1,
	
	CONSTRAINT [PK_Trainer] PRIMARY KEY (Id)
)
