CREATE OR ALTER VIEW dbo.vw_AllClientContacts
AS
	SELECT	C.Id, 
			C.Name,
			C.Surname,
			CONCAT(C.Surname, ' ', C.Name) as [Fullname],
			C.EmailAddress, 
			ISNULL(CCS.[TotalClients], 0) AS [NoOfClients],
			C.CreatedAt,
			C.DeletedAt,
			cc.ClientId,
			CC.ContactId
	FROM dbo.Contacts C WITH(NOLOCK)
	INNER JOIN dbo.ClientContacts CC WITH(NOLOCK) on cc.ContactId = c.Id
	OUTER APPLY(
		SELECT COUNT(*) AS [TotalClients]
		FROM dbo.ClientContacts CC WITH(NOLOCK)
		WHERE CC.ContactId = C.Id
	) AS CCS

