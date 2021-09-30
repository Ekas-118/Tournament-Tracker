
CREATE PROCEDURE dbo.spMatchupEntries_Insert
	@MatchupId int,
	@ParentMatchupId int,
	@TeamCompetingId int,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.MatchupEntries (MatchupId, ParentMatchupId, TeamCompetingId)
	VALUES (@MatchupId, @ParentMatchupId, @TeamCompetingId);

	SELECT @id = SCOPE_IDENTITY();
END
