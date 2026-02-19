CREATE OR ALTER PROC dbo.sp_GetUnlinkedClients
(
	@contactId UNIQUEIDENTIFIER
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	WITH cteLinkedClients AS(
		SELECT DISTINCT C.Id AS [ClientId] 
		FROM dbo.Clients C WITH(NOLOCK)
		INNER JOIN dbo.ClientContacts CC WITH(NOLOCK) ON CC.ClientId = C.Id
		WHERE CC.ContactId = @contactId
	)
	SELECT DISTINCT 
			C.Id, 
			C.Name, 
			C.NameCode, 
			NoOfContacts,
			C.CreatedAt,
			C.DeletedAt
	FROM dbo.vw_AllClients C WITH(NOLOCK)
	WHERE C.Id NOT IN(SELECT cte.[ClientId] FROM cteLinkedClients cte)
END