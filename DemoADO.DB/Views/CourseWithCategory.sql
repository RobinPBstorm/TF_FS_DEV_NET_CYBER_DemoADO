-- Les interêts d'une vue sont
--    * de s'épargner des select dans le code qu'on fera régulièrement, on prépare déjà une vue avec des infos dont on a besoin
--    * d'exposer uniquement certaines données à certaines personnes

CREATE VIEW [dbo].[CourseWithCategory]
	AS 
	SELECT  [Co].[Id], [Co].[Name] AS 'Nom du cours', [Co].[Difficulty] AS 'Difficulté', [Ca].[Name] AS 'Categorie'
	FROM [dbo].[Course] AS [Co] 
		JOIN [dbo].[Category] AS [Ca]
		ON [Co].Category_Id = [Ca].Id
