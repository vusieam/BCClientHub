CREATE OR ALTER PROC dbo.sp_CreateContact
(
	@id UNIQUEIDENTIFIER,
	@name VARCHAR(500),
	@surname VARCHAR(500),
	@emailAddress VARCHAR(500)
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @status BIT, @statusCode INT, @statusMessage VARCHAR(MAX)
	BEGIN TRY
		IF(EXISTS(SELECT * FROM dbo.Contacts C WITH(NOLOCK) WHERE C.EmailAddress = @emailAddress AND C.Id <> @id))
		BEGIN
			SET @status = 0
			SET @statusCode = 409
			SET @statusMessage = 'A contact with the exact same email already exists'
		END
		ELSE
		BEGIN
			INSERT INTO dbo.Contacts(Id, [Name], Surname, EmailAddress)
			VALUES(@id, @name, @surname, @emailAddress);

			SET @status = 1
			SET @statusCode = 200
			SET @statusMessage = 'Successfully created contact'
		END
	END TRY
	BEGIN CATCH
		SET @status = 0
		SET @statusCode = 500
		SET @statusMessage = '<p>Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10))+' <br /> Error Message: ' + ERROR_MESSAGE() +'</p>'
	END CATCH
	SELECT @status [Status], @statusCode [StatusCode], @statusMessage [StatusMessage]

END