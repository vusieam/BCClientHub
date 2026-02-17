CREATE OR ALTER VIEW dbo.vw_AllClients
AS
	SELECT	C.Id, 
			C.Name, 
			C.NameCode, 
			ISNULL(CCS.[TotalContacts], 0) AS [NoOfContacts],
			C.CreatedAt,
			C.DeletedAt
	FROM dbo.Clients C WITH(NOLOCK)
	OUTER APPLY(
		SELECT COUNT(*) AS [TotalContacts]
		FROM dbo.ClientContacts CC WITH(NOLOCK)
		WHERE CC.ClientId = C.Id
	) AS CCS

