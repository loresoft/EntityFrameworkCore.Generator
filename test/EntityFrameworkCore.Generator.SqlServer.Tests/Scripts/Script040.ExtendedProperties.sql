IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties
    WHERE [class] = 0
      AND [major_id] = 0
      AND [minor_id] = 0
      AND [name] = N'GeneratorTest:Environment'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'GeneratorTest:Environment',
        @value = N'IntegrationTests';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[User]')
      AND ep.[minor_id] = 0
      AND ep.[name] = N'MS_Description'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'MS_Description',
        @value = N'Application users.',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'User';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[User]')
      AND ep.[minor_id] = 0
      AND ep.[name] = N'GeneratorTest:AggregateRoot'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'GeneratorTest:AggregateRoot',
        @value = N'True',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'User';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[User]')
      AND ep.[minor_id] = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[User]'), N'EmailAddress', 'ColumnId')
      AND ep.[name] = N'MS_Description'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'MS_Description',
        @value = N'Primary email address for the user.',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'User',
        @level2type = N'COLUMN',
        @level2name = N'EmailAddress';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[StringListUsage]')
  AND ep.[minor_id] = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[StringListUsage]'), N'AnnotatedValues', 'ColumnId')
      AND ep.[name] = N'Generator:SystemType'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'Generator:SystemType',
        @value = N'string[]',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'StringListUsage',
        @level2type = N'COLUMN',
        @level2name = N'AnnotatedValues';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[User]')
      AND ep.[minor_id] = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[User]'), N'EmailAddress', 'ColumnId')
      AND ep.[name] = N'GeneratorTest:IsSensitive'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'GeneratorTest:IsSensitive',
        @value = N'True',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'User',
        @level2type = N'COLUMN',
        @level2name = N'EmailAddress';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    INNER JOIN sys.indexes i
        ON ep.[major_id] = i.[object_id]
       AND ep.[minor_id] = i.[index_id]
    WHERE ep.[class] = 7
      AND i.[object_id] = OBJECT_ID(N'[dbo].[User]')
      AND i.[name] = N'UX_User_EmailAddress'
      AND ep.[name] = N'GeneratorTest:Purpose'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'GeneratorTest:Purpose',
        @value = N'Enforce unique email addresses.',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'TABLE',
        @level1name = N'User',
        @level2type = N'INDEX',
        @level2name = N'UX_User_EmailAddress';
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.extended_properties ep
    WHERE ep.[class] = 1
      AND ep.[major_id] = OBJECT_ID(N'[dbo].[FormatAddress]')
      AND ep.[minor_id] = 0
      AND ep.[name] = N'MS_Description'
)
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'MS_Description',
        @value = N'Formats an address.',
        @level0type = N'SCHEMA',
        @level0name = N'dbo',
        @level1type = N'FUNCTION',
        @level1name = N'FormatAddress';
END;
