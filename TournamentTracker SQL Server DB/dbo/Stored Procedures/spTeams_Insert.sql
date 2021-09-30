
CREATE PROCEDURE dbo.spTeams_Insert
	@TeamName nvarchar(50),
	@id int = 0 output
AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO dbo.Teams (TeamName)
	VALUES (@TeamName);

	SELECT @id = SCOPE_IDENTITY();

END
