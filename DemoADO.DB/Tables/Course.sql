CREATE TABLE [dbo].[Course]
(
	[Id] INT IDENTITY,
	[Name] NVARCHAR(250) NOT NULL,
	[Difficulty] INT NOT NULL,
	[Category_Id] INT NOT NULL,
	CONSTRAINT [PK_Course] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Course_Name] UNIQUE ([Name]),
	CONSTRAINT [CK_Course_Difficulty] CHECK ([Difficulty] BETWEEN 1 AND 5),
	--CONSTRAINT NomContrainte FOREIGN KEY (nom_colonne_ici) REFERENCES TableARelier (Colonne_dans_la_table_a_relier)
	CONSTRAINT [FK_Course_Category] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Category] ([Id])
)
