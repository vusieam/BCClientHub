CREATE OR ALTER PROC dbo.sp_GetClientContacts
(
	@clientId UNIQUEIDENTIFIER
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	SELECT * 
	FROM dbo.vw_AllClientContacts C WITH(NOLOCK)
	WHERE C.ClientId = @clientId
	ORDER BY C.[Fullname]

END