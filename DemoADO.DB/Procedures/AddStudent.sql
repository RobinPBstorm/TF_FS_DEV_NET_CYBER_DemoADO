CREATE PROCEDURE [dbo].[AddStudent]
	@First_Name VARCHAR(50),
	@Last_Name VARCHAR(50),
	@Birth_Date DATE,
	@Year_Result INT,
	@Section_Name VARCHAR(50),
	@Student_Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Section_Id INT;
	SET @Section_Id = (SELECT [Id]
						FROM [dbo].[Section]
						WHERE LOWER(@Section_Name) = LOWER([SectionName]));
	
	IF (@Section_Id is null)
	BEGIN
		INSERT INTO [dbo].[Section] ([SectionName]) VALUES (@Section_Name);
		
		SET @Section_Id = SCOPE_IDENTITY();
	END


	INSERT INTO [dbo].[Student] ([FirstName], [LastName], [BirthDate], [YearResult], [SectionId])
	VALUES (@First_Name, @Last_Name,@Birth_Date, @Year_Result, @Section_Id);
	
	SET @Student_Id = SCOPE_IDENTITY();
END
