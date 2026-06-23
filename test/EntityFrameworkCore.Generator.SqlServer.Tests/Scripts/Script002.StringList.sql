IF NOT EXISTS (
    SELECT 1
    FROM sys.types t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.is_user_defined = 1
      AND t.name = N'StringList'
      AND s.name = N'dbo'
)
BEGIN
    CREATE TYPE [dbo].[StringList] FROM NVARCHAR(MAX) NULL;
END;
