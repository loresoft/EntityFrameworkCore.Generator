IF NOT EXISTS (
    SELECT 1
    FROM sys.types t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.is_table_type = 1
      AND t.name = N'IdentifierTable'
      AND s.name = N'dbo'
)
BEGIN
    CREATE TYPE [dbo].[IdentifierTable] AS TABLE
    (
        [Id] INT NOT NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END;

