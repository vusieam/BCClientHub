CREATE OR ALTER PROC dbo.sp_LinkContact
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
		IF(EXISTS(SELECT * FROM dbo.ClientContacts C WITH(NOLOCK) WHERE C.ClientId = @clientId AND C.ContactId = @contactId))
		BEGIN
			SET @status = 0
			SET @statusCode = 409
			SET @statusMessage = 'The contact is already linked to this client'
		END
		ELSE
		BEGIN
			INSERT INTO dbo.ClientContacts([ClientId], [ContactId])
			VALUES(@clientId, @contactId);

			SET @status = 1
			SET @statusCode = 200
			SET @statusMessage = 'Successfully linked contact to client'
		END
	END TRY
	BEGIN CATCH
		SET @status = 0
		SET @statusCode = 500
		SET @statusMessage = '<p>Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10))+' <br /> Error Message: ' + ERROR_MESSAGE() +'</p>'
	END CATCH
	SELECT @status [Status], @statusCode [StatusCode], @statusMessage [StatusMessage]

END