--Attention à bien sélectionner Script Post Deployment
--Script qui va se lancer après création de la DB, on va donc y mettre les instructions pour remplir notre DB

---------------------------------------------------
-- Category
---------------------------------------------------

SET IDENTITY_INSERT [dbo].[Category] ON
--Désactive l'auto-incrémentation, comme ça on peut mettre les id qu'on veut

INSERT INTO [dbo].[Category] ([Id], [Name]) VALUES
(1, 'Développement'),
(2, 'Data')

SET IDENTITY_INSERT [dbo].[Category] OFF
--Réactive l'auto-incrémentation

---------------------------------------------------
-- Course
---------------------------------------------------
SET IDENTITY_INSERT [dbo].[Course] ON;

INSERT INTO [dbo].[Course] ([Id], [Name], [Difficulty], [Category_Id]) VALUES 
(1, 'Algorithmique', 2, 1),
(2, 'SSIS', 5 , 2),
(3, 'SQL Prodécural', 5, 1),
(4, 'C# Orienté Objet', 3, 1)

SET IDENTITY_INSERT [dbo].[Course] OFF;

---------------------------------------------------
-- Trainer
---------------------------------------------------
SET IDENTITY_INSERT [dbo].[Trainer] ON;

INSERT INTO [dbo].[Trainer] ([Id], [FirstName], [LastName], [BirthDate]) VALUES
(1, 'Aude', 'Beurive', '1989-10-16'),
(2, 'Gavin', 'Chaineux', '1992-10-18' ),
(3, 'Thierry', 'Morre', NULL)

SET IDENTITY_INSERT [dbo].[Trainer] OFF;

---------------------------------------------------
--MM Trainer Course
---------------------------------------------------
INSERT INTO [dbo].[MM_Trainer_Course] ([Trainer_Id], [Course_Id]) VALUES 
(1, 1),
(1, 3),
(1, 4),
(2, 1),
(2, 3),
(2, 4),
(3, 1),
(3, 3),
(3, 4)

---------
-- Section
---------
SET IDENTITY_INSERT [dbo].[Section] ON;

INSERT INTO [dbo].[Section] ([ID], [SectionName]) VALUES 
(1, 'Full Stack Dev'),
(2, 'Business Analyst');

SET IDENTITY_INSERT [dbo].[Section] OFF;

---------
-- Student
---------
SET IDENTITY_INSERT [dbo].[Student] ON;

INSERT INTO [dbo].[Student] ([ID],[FirstName], [LastName], [BirthDate], [YearResult], [SectionID]) VALUES 
(1, 'Steve', 'Jobs', '1955-02-24', 12, 2),
(2, 'Bill', 'Gates', '1955-10-28', 14, 1);

SET IDENTITY_INSERT [dbo].[Student] OFF;