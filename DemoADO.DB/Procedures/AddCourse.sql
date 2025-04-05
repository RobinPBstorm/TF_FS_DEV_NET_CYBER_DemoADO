CREATE PROCEDURE [dbo].[AddCourse]
	@Course_Name NVARCHAR(250),
	@Course_Difficulty INT,
	@Category_Name NVARCHAR(100),
	@Course_Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON; --Désactive le retour de nombres de lignes affectées

	DECLARE @Category_Id INT;
	--On récupère l'id de la catégorie, donc le nom sera identique au nom reçu en param
	--Si aucun ne correspond, @Category_Id sera NULL
	SET @Category_Id = (SELECT [Id] 
						FROM [dbo].[Category]
						WHERE [Name] = @Category_Name)

	--Si le nom de la catégorie n'est pas déjà en DB, on l'ajoute, on récupère l'id et enfin, on l'ajoutera au cours
	IF(@Category_Id IS NULL)
	BEGIN
		--On insère la catégorie dans la table catégorie
		INSERT INTO [dbo].[Category] ([Name]) VALUES (@Category_Name);
		
		SET @Category_Id = SCOPE_IDENTITY();
		--SCOPE_IDENTITY() vous renvoie le dernier id auto-incrémenté qui vient d'être généré
	END

	-- Insérer le cours puisqu'on a maintenant l'id de la catégorie
	INSERT INTO [dbo].[Course] ([Name], [Difficulty], [Category_Id])
	VALUES (@Course_Name, @Course_Difficulty, @Category_Id);

	--On met dans la variable de sortie l'id du cours qui vient d'être créé
	SET @Course_Id = SCOPE_IDENTITY();
END


