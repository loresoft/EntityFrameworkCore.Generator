IF OBJECT_ID(N'[dbo].[StringListUsage]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[StringListUsage]
    (
        [Id] INT IDENTITY(1, 1) NOT NULL,
        [Values] [dbo].[StringList] NULL,
        CONSTRAINT [PK_StringListUsage] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END;
