CREATE OR ALTER PROC dbo.sp_DeLinkContact
(
	@clientId UNIQUEIDENTIFIER,
	@contactId UNIQUEIDENTIFIER
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @status BIT, @statusCode INT, @statusMessage VARCHAR(MAX)
	BEGIN TRY
		IF(NOT EXISTS(SELECT * FROM dbo.ClientContacts C WITH(NOLOCK) WHERE C.ClientId = @clientId AND C.ContactId = @contactId))
		BEGIN
			SET @status = 0
			SET @statusCode = 404
			SET @statusMessage = 'The contact has already been delinked from the client'
		END
		ELSE
		BEGIN
			DELETE T
			FROM dbo.ClientContacts T WITH(NOLOCK)
			WHERE T.ClientId = @clientId AND T.ContactId = @contactId

			SET @status = 1
			SET @statusCode = 200
			SET @statusMessage = 'Successfully delinked contact from client'
		END
	END TRY
	BEGIN CATCH
		SET @status = 0
		SET @statusCode = 500
		SET @statusMessage = '<p>Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10))+' <br /> Error Message: ' + ERROR_MESSAGE() +'</p>'
	END CATCH
	SELECT @status [Status], @statusCode [StatusCode], @statusMessage [StatusMessage]

END