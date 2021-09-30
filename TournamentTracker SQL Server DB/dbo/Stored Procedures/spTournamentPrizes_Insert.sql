
CREATE PROCEDURE dbo.spTournamentPrizes_Insert
	@TournamentId int,
	@PrizeId int,
	@id int = 0 output

AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.TournamentPrizes (TournamentId, PrizeId)
	VALUES (@TournamentId, @PrizeId);

	SELECT @id = SCOPE_IDENTITY();
END
