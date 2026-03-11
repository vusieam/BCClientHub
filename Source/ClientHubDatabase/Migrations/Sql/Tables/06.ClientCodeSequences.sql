IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClientCodeSequences')
BEGIN
    CREATE TABLE [dbo].[ClientCodeSequences]
    (
	    [Prefix] [varchar](3) NOT NULL,
	    [CurrentValue] [int] NOT NULL default (1),
	    [CreatedAt] [datetime] NOT NULL DEFAULT GETDATE(),
	    [UpdatedAt] [datetime] NULL,
        CONSTRAINT PK_ClientCodeSequences PRIMARY KEY CLUSTERED (Prefix ASC), 
        INDEX IX_ClientCodeSequences ([CurrentValue], [CreatedAt], [UpdatedAt])
    )
END