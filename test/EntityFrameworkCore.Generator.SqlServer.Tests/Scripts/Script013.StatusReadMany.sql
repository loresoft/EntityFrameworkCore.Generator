CREATE OR ALTER PROCEDURE [dbo].[StatusReadMany]
(
    @Identifiers [dbo].[IdentifierTable] READONLY
)
AS
BEGIN

SELECT
    s.[Id],
    s.[Name],
    s.[Description],
    s.[DisplayOrder],
    s.[IsActive],
    s.[Created],
    s.[CreatedBy],
    s.[Updated],
    s.[UpdatedBy],
    s.[RowVersion]
FROM [dbo].[Status] AS s
INNER JOIN @Identifiers AS i
    ON s.[Id] = i.[ID]

END;
