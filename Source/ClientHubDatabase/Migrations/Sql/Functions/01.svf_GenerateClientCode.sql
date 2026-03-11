CREATE OR ALTER FUNCTION dbo.svf_GenerateClientCode
(
	@ClientName VARCHAR(500)
) RETURNS VARCHAR(3)
WITH ENCRYPTION
BEGIN
	DECLARE @Prefix VARCHAR(3) = 
	UPPER
        (
            CASE 
                WHEN(LEN(@ClientName) <= 3)
                THEN
                (
                    CASE 
                        WHEN LEN(LEFT(@ClientName,3)) = 1 THEN LEFT(@ClientName,1) + 'AB'
                        WHEN LEN(LEFT(@ClientName,3)) = 2 THEN LEFT(@ClientName,2) + 'A'
                        ELSE @ClientName
                    END                    
                )
                ELSE
                (
                    LEFT
                    (
                        CASE
                            -- Two words exactly
                            WHEN LEN(LTRIM(RTRIM(@ClientName))) - LEN(REPLACE(LTRIM(RTRIM(@ClientName)), ' ', '')) = 1
                            THEN
                                (
                                    SELECT (LEFT(PARSENAME(REPLACE(@ClientName,' ','.'),2),1) + LEFT(PARSENAME(REPLACE(@ClientName,' ','.'),1),2))
                                )
                            -- More than two words
                            WHEN LEN(LTRIM(RTRIM(@ClientName))) - LEN(REPLACE(LTRIM(RTRIM(@ClientName)), ' ', '')) > 1
                            THEN
                                (
                                    SELECT TOP 3 STRING_AGG(LEFT(value,1), '')
                                    FROM STRING_SPLIT(@ClientName, ' ')
                                )

                            -- Single word
                            ELSE @ClientName
                        END
                    , 3)
                )               
            END
        )
        RETURN @Prefix
END