-- Création d'un trigger qui va s'activer quand on va vouloir supprimer un formateur, pour simplement le désactiver à la place
CREATE TRIGGER [TR_TRAINER_SOFT_DELETE] --Nom du trigger
ON [dbo].[Trainer] --Nom de la table sur laquelle se fait le trigger
	INSTEAD OF DELETE --Sur quoi on veut déclencher le trigger (insert, update, delete)
AS
BEGIN
	SET NOCOUNT ON; --Pour que le traitement ne nous renvoie pas le nombres de lignes modifiées (c'est juste pour gagner en opti)

	UPDATE [dbo].[Trainer]
	SET [IsActive] = 0
	WHERE [Id] = (SELECT [Id] FROM deleted)
	-- deleted est une table temporaire créée pendant la suppression (et la modification)
	-- il existe aussi la table inserted créée à l'insertion (et à la modification)
END