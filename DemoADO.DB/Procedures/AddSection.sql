CREATE PROCEDURE [dbo].[AddSection]
	@Section_Name VARCHAR(50),
	@Section_Id INT OUTPUT
AS
BEGIN 
	SET NOCOUNT ON;

	-- La section est-elle déjà présente ?
	SET @Section_Id = (SELECT [Id]
						FROM [dbo].[Section]
						WHERE LOWER(@Section_Name) = LOWER([SectionName]));
	
	IF (@Section_Id is null)
	BEGIN
		INSERT INTO [dbo].[Section] ([SectionName]) VALUES (@Section_Name);
		
		SET @Section_Id = SCOPE_IDENTITY();
	END

	INSERT INTO [dbo].[Section] ([SectionName])
	VALUES (@Section_Name);

	SET @Section_Id = SCOPE_IDENTITY();
END
