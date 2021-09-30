
CREATE PROCEDURE [dbo].[spTournaments_Insert]
	@TournamentName nvarchar(200),
	@EntryFee money,
	@InitialPrizePool money,
	@GreaterScoreWins bit,
	@Id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.Tournaments (TournamentName, EntryFee, InitialPrizePool, GreaterScoreWins, Active)
	VALUES (@TournamentName, @EntryFee, @InitialPrizePool, @GreaterScoreWins, 1);

	select @id = SCOPE_IDENTITY();
END
