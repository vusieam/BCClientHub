CREATE OR ALTER TRIGGER dbo.trg_generate_client_code
ON dbo.Clients
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE c
    SET c.NameCode =
        UPPER
        (
            CASE 
                WHEN(LEN(i.Name) <= 3)
                THEN
                (
                    CASE 
                        WHEN LEN(LEFT(i.Name,3)) = 1 THEN LEFT(i.Name,1) + 'AB'
                        WHEN LEN(LEFT(i.Name,3)) = 2 THEN LEFT(i.Name,2) + 'A'
                        ELSE i.Name
                    END                    
                )
                ELSE
                (
                    LEFT
                    (
                        CASE
                            -- Two words exactly
                            WHEN LEN(LTRIM(RTRIM(i.Name))) - LEN(REPLACE(LTRIM(RTRIM(i.Name)), ' ', '')) = 1
                            THEN
                                (
                                    SELECT (LEFT(PARSENAME(REPLACE(i.Name,' ','.'),2),1) + LEFT(PARSENAME(REPLACE(i.Name,' ','.'),1),2))
                                )
                            -- More than two words
                            WHEN LEN(LTRIM(RTRIM(i.Name))) - LEN(REPLACE(LTRIM(RTRIM(i.Name)), ' ', '')) > 1
                            THEN
                                (
                                    SELECT TOP 3 STRING_AGG(LEFT(value,1), '')
                                    FROM STRING_SPLIT(i.Name, ' ')
                                )

                            -- Single word
                            ELSE i.Name
                        END
                    , 3)
                )               
            END
        )
        + 
        (FORMAT(NEXT VALUE FOR dbo.ClientCodeSequence, 'D3'))

    FROM dbo.Clients c WITH(NOLOCK)
    INNER JOIN inserted i ON c.Id = i.Id;
END;
