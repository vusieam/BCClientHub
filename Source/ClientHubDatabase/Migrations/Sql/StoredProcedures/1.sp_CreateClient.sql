CREATE OR ALTER PROC dbo.sp_CreateClient
(
	@name VARCHAR(500)
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @status BIT, @statusCode INT, @statusMessage VARCHAR(MAX)
	BEGIN TRY
		IF(EXISTS(SELECT * FROM dbo.Clients C WITH(NOLOCK) WHERE C.[Name] = @name))
		BEGIN
			SET @status = 0
			SET @statusCode = 409
			SET @statusMessage = 'Client with the exact same name already exists'
		END
		ELSE
		BEGIN
			INSERT INTO dbo.Clients([Name], NameCode)
			VALUES(@name, '');

			SET @status = 1
			SET @statusCode = 200
			SET @statusMessage = 'Successfully created client'
		END
	END TRY
	BEGIN CATCH
		SET @status = 0
		SET @statusCode = 500
		SET @statusMessage = '<p>Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10))+' <br /> Error Message: ' + ERROR_MESSAGE() +'</p>'
	END CATCH
	--SELECT @status, @statusCode, @statusMessage
	SELECT @status [Status], @statusCode [StatusCode], @statusMessage [StatusMessage]

END