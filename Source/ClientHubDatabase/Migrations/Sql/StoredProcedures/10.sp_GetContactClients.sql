CREATE OR ALTER PROC dbo.sp_GetContactClients
(
	@contactId UNIQUEIDENTIFIER
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	SELECT * 
	FROM dbo.vw_AllContactClients C WITH(NOLOCK)
	WHERE C.ContactId = @contactId AND C.DeletedAt IS NULL
	ORDER BY C.[Name]

END