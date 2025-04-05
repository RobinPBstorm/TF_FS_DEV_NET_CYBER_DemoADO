CREATE PROCEDURE [dbo].[DeleteStudent]
	@Student_Id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Student]
	WHERE [Id] = @Student_Id
END