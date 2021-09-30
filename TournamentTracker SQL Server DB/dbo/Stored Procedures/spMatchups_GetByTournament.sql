
CREATE PROCEDURE dbo.spMatchups_GetByTournament
	@TournamentId int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT m.*
	FROM dbo.Matchups m
	WHERE m.TournamentId = @TournamentId
	ORDER BY MatchupRound
END
