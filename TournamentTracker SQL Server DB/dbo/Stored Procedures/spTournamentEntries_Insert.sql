
CREATE PROCEDURE dbo.spTournamentEntries_Insert
	@TournamentId int,
	@TeamId int,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.TournamentEntries (TournamentId, TeamId)
	VALUES (@TournamentId, @TeamId);

	SELECT @id = SCOPE_IDENTITY();
END
