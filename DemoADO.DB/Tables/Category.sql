CREATE TABLE [dbo].[Category]
(
	[Id] INT IDENTITY,
    [Name] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Category_Name] UNIQUE ([Name])
)
