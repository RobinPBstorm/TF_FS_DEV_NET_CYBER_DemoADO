CREATE TABLE [dbo].[MM_Trainer_Course]
(
	[Trainer_Id] INT NOT NULL,
	[Course_Id] INT NOT NULL,
	CONSTRAINT [FK_MM_Trainer] FOREIGN KEY ([Trainer_Id]) REFERENCES [dbo].[Trainer] ([Id]),
	CONSTRAINT [FK_MM_Course] FOREIGN KEY ([Course_Id]) REFERENCES [dbo].[Course] ([Id]),
	CONSTRAINT [PK_MM_Trainer_Course] PRIMARY KEY ([Trainer_Id], [Course_Id])
)
