-- Gets the people on a given team
CREATE PROCEDURE [dbo].[spTeamMembers_GetByTeam]
	@TeamId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.*
	FROM TeamMembers tm
	JOIN People p ON tm.PersonId = p.id
	WHERE tm.TeamId = @TeamId
END
