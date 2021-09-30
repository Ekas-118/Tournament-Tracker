
CREATE PROCEDURE dbo.spTournaments_Complete
	@id int
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dbo.Tournaments
	SET Active = 0
	WHERE id = @id;
END
