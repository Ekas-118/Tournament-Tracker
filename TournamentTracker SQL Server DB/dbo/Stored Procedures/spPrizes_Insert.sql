
CREATE PROCEDURE [dbo].[spPrizes_Insert]
	@PlaceNumber int,
	@PlaceName nvarchar(50),
	@PrizeAmount money,
	@PrizePercentage float,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.Prizes(PlaceNumber, PlaceName, PrizeAmount, PrizePercentage)
	VALUES
	(@PlaceNumber, @PlaceName, @PrizeAmount, @PrizePercentage);

	SELECT @id = SCOPE_IDENTITY();

END
