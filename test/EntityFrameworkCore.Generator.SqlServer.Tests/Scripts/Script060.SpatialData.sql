IF OBJECT_ID(N'[dbo].[SpatialData]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SpatialData]
    (
        [Id] INT NOT NULL CONSTRAINT [PK_SpatialData] PRIMARY KEY,
        [GeometryValue] GEOMETRY NULL,
        [GeographyValue] GEOGRAPHY NULL,
        [HierarchyValue] HIERARCHYID NULL
    );
END;
