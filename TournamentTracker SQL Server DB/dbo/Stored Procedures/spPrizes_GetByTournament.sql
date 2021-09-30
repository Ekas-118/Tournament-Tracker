
CREATE PROCEDURE dbo.spPrizes_GetByTournament
	@TournamentId int
AS
BEGIN
	SET NOCOUNT ON;

    select p.*
	from Prizes p
	join TournamentPrizes t on t.PrizeId = p.id
	where t.TournamentId = @TournamentId
END
