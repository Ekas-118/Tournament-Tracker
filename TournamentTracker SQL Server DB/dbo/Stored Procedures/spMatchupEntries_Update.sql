
CREATE PROCEDURE dbo.spMatchupEntries_Update
	@id int,
	@TeamCompetingId int = null,
	@Score float = null
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dbo.MatchupEntries
	SET TeamCompetingId = @TeamCompetingId,
	Score = @Score
	WHERE id = @id
END
