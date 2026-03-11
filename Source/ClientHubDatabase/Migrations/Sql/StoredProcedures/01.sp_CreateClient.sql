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

			DECLARE @Prefix VARCHAR(3) = (SELECT dbo.svf_GenerateClientCode(@name)),
					@CurrentValue INT = 1000
			IF(NOT EXISTS(SELECT * FROM dbo.ClientCodeSequences G WITH(NOLOCK) WHERE G.Prefix = @Prefix))
			BEGIN
				INSERT INTO dbo.ClientCodeSequences(Prefix)
				VALUES(@Prefix)

				SELECT @CurrentValue = G.CurrentValue 
				FROM dbo.ClientCodeSequences G WITH(NOLOCK) WHERE G.Prefix = @Prefix
			END
			ELSE
			BEGIN
				IF((SELECT G.CurrentValue FROM dbo.ClientCodeSequences G WITH(NOLOCK) WHERE G.Prefix = @Prefix) < 999)
				BEGIN					
					UPDATE  T
					SET T.CurrentValue = T.CurrentValue + 1, T.UpdatedAt = GETDATE()
					FROM dbo.ClientCodeSequences T WITH(NOLOCK)
					WHERE T.Prefix = @Prefix

					SELECT @CurrentValue = G.CurrentValue 
					FROM dbo.ClientCodeSequences G WITH(NOLOCK) WHERE G.Prefix = @Prefix
				END
			END

			IF(@CurrentValue > 999)
			BEGIN
				SET @Status = 0
				SET @statusCode = 500
				SET @statusMessage = CONCAT('Sequence limit of 999 has been reached prefix: ', @Prefix)
			END
			ELSE
			BEGIN
				--SET @Status = 1
				--SET @Message = 'Successful'
				DECLARE @ClientCode VARCHAR(8) = @Prefix + FORMAT(@CurrentValue, 'D3')
				

				INSERT INTO dbo.Clients([Name], Code)
				VALUES(@name, @ClientCode);

				SET @status = 1
				SET @statusCode = 200
				SET @statusMessage = 'Successfully created client'
			END
		END
	END TRY
	BEGIN CATCH
		SET @status = 0
		SET @statusCode = 500
		SET @statusMessage = '<p>Error Line: '+ CAST(ERROR_LINE() AS VARCHAR(10))+' <br /> Error Message: ' + ERROR_MESSAGE() +'</p>'
	END CATCH
	SELECT @status [Status], @statusCode [StatusCode], @statusMessage [StatusMessage]

END