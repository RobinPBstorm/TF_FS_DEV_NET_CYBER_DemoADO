CREATE PROCEDURE [dbo].[UpdateStudent]
	@Student_Id int,
	@Section_Id int,
	@Year_Result int
AS
	UPDATE [dbo].[Student]
	SET [SectionId] = @Section_Id, [YearResult] = @Year_Result
	WHERE [Id] = @Student_Id;
RETURN 0
