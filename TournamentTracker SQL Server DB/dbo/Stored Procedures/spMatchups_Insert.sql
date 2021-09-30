
CREATE PROCEDURE dbo.spMatchups_Insert
	@TournamentId int,
	@MatchupRound int,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.Matchups (TournamentId, MatchupRound)
	VALUES (@TournamentId, @MatchupRound);

	SELECT @id = SCOPE_IDENTITY();
END
