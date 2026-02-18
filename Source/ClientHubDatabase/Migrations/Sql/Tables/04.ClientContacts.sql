IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClientContacts')
BEGIN
    CREATE TABLE dbo.ClientContacts 
    (
        ClientId UNIQUEIDENTIFIER NOT NULL,
        ContactId UNIQUEIDENTIFIER NOT NULL,
        LinkedAt DATETIME NOT NULL DEFAULT GETDATE(),
        DeLinkedAt DATETIME NULL,
		CONSTRAINT PK_ClientContacts PRIMARY KEY CLUSTERED (ClientId ASC, ContactId ASC),        
        CONSTRAINT FK_ClientContacts_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id)
        ON DELETE CASCADE,
        CONSTRAINT FK_ClientContacts_Contacts FOREIGN KEY (ContactId) REFERENCES Contacts(Id)
        ON DELETE CASCADE,
        INDEX IX_ClientContacts (LinkedAt, DeLinkedAt)
    )
END