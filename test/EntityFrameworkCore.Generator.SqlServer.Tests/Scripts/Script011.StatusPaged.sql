CREATE OR ALTER PROCEDURE [dbo].[StatusPaged]
    @Offset INT = 0,
    @Size INT = 100,
    @Total BIGINT OUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @Total = (
        SELECT COUNT(t.[Id])
        FROM [dbo].[Status] AS t
    );

    SELECT
        t.[Id],
        t.[Name],
        t.[Description],
        t.[DisplayOrder],
        t.[IsActive],
        t.[Created],
        t.[CreatedBy],
        t.[Updated],
        t.[UpdatedBy]
    FROM [dbo].[Status] AS t
    ORDER BY t.[Id]
    OFFSET @Offset ROWS
    FETCH NEXT @Size ROWS ONLY;

    SET NOCOUNT OFF;
END
