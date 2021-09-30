
CREATE PROCEDURE [dbo].[spMatchupEntries_GetByMatchup]
	@MatchupId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM MatchupEntries
	WHERE MatchupId = @MatchupId
END
