
CREATE PROCEDURE dbo.spPeople_Insert
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@EmailAddress nvarchar(200),
	@CellphoneNumber varchar(20),
	@Id int = 0 output
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO dbo.People (FirstName, LastName, EmailAddress, CellphoneNumber)
	VALUES
	(@FirstName, @LastName, @EmailAddress, @CellphoneNumber);

	SELECT @id = SCOPE_IDENTITY();

END
