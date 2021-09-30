
CREATE PROCEDURE dbo.spTeam_GetByTournament
	@TournamentId int
AS
BEGIN
	SET NOCOUNT ON;

    select t.*
	from Teams t
	join TournamentEntries e on e.TeamId = t.id
	where e.TournamentId = @TournamentId
END
