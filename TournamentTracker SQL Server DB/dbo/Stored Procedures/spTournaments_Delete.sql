
CREATE PROCEDURE [dbo].[spTournaments_Delete]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Prizes
	WHERE Id IN (SELECT Id FROM dbo.TournamentPrizes WHERE TournamentId = @id);

    DELETE FROM dbo.Tournaments
	WHERE Id = @id;
END
