CREATE TABLE [dbo].[Student]
(
	[ID] INT PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[YearResult] INT NOT NULL,
	[SectionID] INT NOT NULL,
	[IsActive] BIT DEFAULT 1,

	CONSTRAINT Fk_Student_Section FOREIGN KEY ([SectionID]) REFERENCES [Section] ([ID]),
	CONSTRAINT Ck_Student_YearResult CHECK ([YearResult] BETWEEN 0 AND 20),
	CONSTRAINT Ck_Student_BirthDate CHECK ([BirthDate] >= '1930-01-01')
);
