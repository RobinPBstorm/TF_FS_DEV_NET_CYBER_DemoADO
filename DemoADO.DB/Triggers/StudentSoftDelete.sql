CREATE TRIGGER [dbo].[StudentSoftDelete]
ON [dbo].[Student]
	INSTEAD OF DELETE
AS
BEGIN	
	SET NOCOUNT ON;

	UPDATE [dbo].[Student]
	SET [IsActive] = 0
	WHERE [Id] = (SELECT [Id] FROM deleted)
END
