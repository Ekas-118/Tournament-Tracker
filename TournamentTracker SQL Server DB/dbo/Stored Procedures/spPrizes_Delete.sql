CREATE PROCEDURE spPrizes_Delete
	@id int
AS
BEGIN
	DELETE FROM dbo.Prizes
	WHERE id = @id
END
