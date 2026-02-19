CREATE OR ALTER VIEW dbo.vw_AllContactClients
AS
	SELECT	C.Id, 
			C.Name, 
			C.NameCode, 
			ISNULL(CCS.[TotalContacts], 0) AS [NoOfContacts],
			C.CreatedAt,
			C.DeletedAt,
			cc.ClientId,
			CC.ContactId
	FROM dbo.Clients C WITH(NOLOCK)
	INNER JOIN dbo.ClientContacts CC WITH(NOLOCK) on cc.ClientId = c.Id
	OUTER APPLY(
		SELECT COUNT(*) AS [TotalContacts]
		FROM dbo.ClientContacts CC WITH(NOLOCK)
		WHERE CC.ClientId = C.Id
	) AS CCS

