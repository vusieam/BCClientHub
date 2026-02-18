CREATE OR ALTER PROC dbo.sp_GetUnlinkedContacts
(
	@clientId UNIQUEIDENTIFIER
)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	WITH cteLinkedContacts AS(
		SELECT DISTINCT C.Id AS [ContactId] 
		FROM dbo.Contacts C WITH(NOLOCK)
		INNER JOIN dbo.ClientContacts CC WITH(NOLOCK) ON CC.ContactId = C.Id
		WHERE CC.ClientId = @clientId
	)
	SELECT DISTINCT 
			C.Id, 
			C.Name,
			C.Surname,
			CONCAT(C.Surname, ' ', C.Name) as [Fullname],
			C.EmailAddress, 
			ISNULL(CCS.[TotalClients], 0) AS [NoOfClients],
			C.CreatedAt,
			C.DeletedAt 
	FROM dbo.Contacts C WITH(NOLOCK)
	OUTER APPLY(
		SELECT COUNT(*) AS [TotalClients]
		FROM dbo.ClientContacts CC WITH(NOLOCK)
		WHERE CC.ContactId = C.Id
	) AS CCS
	WHERE C.Id NOT IN(SELECT cte.[ContactId] FROM cteLinkedContacts cte)


	--SELECT C.* 
	--FROM dbo.Contacts C WITH(NOLOCK)
	--INNER JOIN dbo.ClientContacts CC WITH(NOLOCK) ON CC.ContactId = C.Id
	--WHERE CC.ClientId <>@clientId
END