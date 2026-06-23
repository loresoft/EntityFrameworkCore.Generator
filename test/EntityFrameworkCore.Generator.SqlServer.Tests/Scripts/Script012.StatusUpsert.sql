CREATE OR ALTER PROCEDURE [dbo].[StatusUpsert]
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @DisplayOrder INT,
    @IsActive BIT,
    @Created DATETIMEOFFSET,
    @CreatedBy NVARCHAR(100) = NULL,
    @Updated DATETIMEOFFSET,
    @UpdatedBy NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    MERGE INTO [dbo].[Status] WITH (UPDLOCK, SERIALIZABLE) AS t
    USING
    (
        SELECT
            @Id,
            @Name,
            @Description,
            @DisplayOrder,
            @IsActive,
            @Created,
            @CreatedBy,
            @Updated,
            @UpdatedBy
    )
    AS s
    (
        [Id],
        [Name],
        [Description],
        [DisplayOrder],
        [IsActive],
        [Created],
        [CreatedBy],
        [Updated],
        [UpdatedBy]
    )
    ON
    (
        t.[Id] = s.[Id]
    )
    WHEN NOT MATCHED BY TARGET THEN
        INSERT
        (
            [Id],
            [Name],
            [Description],
            [DisplayOrder],
            [IsActive],
            [Created],
            [CreatedBy],
            [Updated],
            [UpdatedBy]
        )
        VALUES
        (
            s.[Id],
            s.[Name],
            s.[Description],
            s.[DisplayOrder],
            s.[IsActive],
            s.[Created],
            s.[CreatedBy],
            s.[Updated],
            s.[UpdatedBy]
        )
    WHEN MATCHED THEN
        UPDATE SET
            t.[Name] = s.[Name],
            t.[Description] = s.[Description],
            t.[DisplayOrder] = s.[DisplayOrder],
            t.[IsActive] = s.[IsActive],
            t.[Created] = s.[Created],
            t.[CreatedBy] = s.[CreatedBy],
            t.[Updated] = s.[Updated],
            t.[UpdatedBy] = s.[UpdatedBy]
    OUTPUT
        INSERTED.[Id],
        INSERTED.[Name],
        INSERTED.[Description],
        INSERTED.[DisplayOrder],
        INSERTED.[IsActive],
        INSERTED.[Created],
        INSERTED.[CreatedBy],
        INSERTED.[Updated],
        INSERTED.[UpdatedBy],
        INSERTED.[RowVersion];

    COMMIT TRANSACTION;

    SET NOCOUNT OFF;
END
