IF OBJECT_ID(N'[dbo].[TemporalTask]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TemporalTask]
    (
        [Id] INT NOT NULL CONSTRAINT [PK_TemporalTask] PRIMARY KEY,
        [Title] NVARCHAR(100) NOT NULL,
        [ValidFrom] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
        [ValidTo] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
        PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
    )
    WITH
    (
        SYSTEM_VERSIONING = ON
        (
            HISTORY_TABLE = [dbo].[TemporalTaskHistory]
        )
    );
END;
