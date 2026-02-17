IF(NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'ClientCodeSequence' AND SCHEMA_NAME(schema_id) = 'dbo'))
BEGIN
    CREATE SEQUENCE dbo.ClientCodeSequence
    AS INT
    START WITH 1
    INCREMENT BY 1
END