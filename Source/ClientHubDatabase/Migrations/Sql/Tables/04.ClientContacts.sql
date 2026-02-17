IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClientContacts')
BEGIN
    CREATE TABLE dbo.ClientContacts 
    (
        ClientId NVARCHAR(255) NOT NULL,
        ContactId NVARCHAR(255) UNIQUE NOT NULL,
        LinkedAt DATETIME NOT NULL DEFAULT GETDATE(),
        DeLinkedAt DATETIME NULL,
		CONSTRAINT PK_ClientContacts PRIMARY KEY CLUSTERED (ClientId ASC, ContactId ASC),
        INDEX IX_ClientContacts (LinkedAt, DeLinkedAt)
    )
END