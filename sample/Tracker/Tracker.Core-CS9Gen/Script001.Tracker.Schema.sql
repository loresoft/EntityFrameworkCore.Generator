-- Schema
-- IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Tracker')
-- EXEC sys.sp_executesql N'CREATE SCHEMA [dbo] AUTHORIZATION [dbo]'
-- GO

-- IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Identity')
-- EXEC sys.sp_executesql N'CREATE SCHEMA [dbo] AUTHORIZATION [dbo]'
-- GO

-- Tables
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit]') AND type in (N'U'))
CREATE TABLE [dbo].[Audit] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [Date] datetime NOT NULL,
    [UserId] uniqueidentifier NULL,
    [TaskId] uniqueidentifier NULL,
    [Content] nvarchar(MAX) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Priority]') AND type in (N'U'))
CREATE TABLE [dbo].[Priority] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [DisplayOrder] int NOT NULL DEFAULT (0),
    [IsActive] bit NOT NULL DEFAULT (1),
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Priority] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
CREATE TABLE [dbo].[Role] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [Name] nvarchar(256) NOT NULL,
    [Description] nvarchar(MAX) NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
CREATE TABLE [dbo].[Status] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [DisplayOrder] int NOT NULL DEFAULT (0),
    [IsActive] bit NOT NULL DEFAULT (1),
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
CREATE TABLE [dbo].[Task] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [StatusId] uniqueidentifier NOT NULL,
    [PriorityId] uniqueidentifier NULL,
    [Title] nvarchar(255) NOT NULL,
    [Description] nvarchar(MAX) NULL,
    [StartDate] datetimeoffset NULL,
    [DueDate] datetimeoffset NULL,
    [CompleteDate] datetimeoffset NULL,
    [AssignedId] uniqueidentifier NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskExtended]') AND type in (N'U'))
CREATE TABLE [dbo].[TaskExtended] (
    [TaskId] uniqueidentifier NOT NULL,
    [UserAgent] nvarchar(MAX) NULL,
    [Browser] nvarchar(256) NULL,
    [OperatingSystem] nvarchar(256) NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_TaskExtended] PRIMARY KEY ([TaskId])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
CREATE TABLE [dbo].[User] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [EmailAddress] nvarchar(256) NOT NULL,
    [IsEmailAddressConfirmed] bit NOT NULL DEFAULT (0),
    [DisplayName] nvarchar(256) NOT NULL,
    [PasswordHash] nvarchar(MAX) NULL,
    [ResetHash] nvarchar(MAX) NULL,
    [InviteHash] nvarchar(MAX) NULL,
    [AccessFailedCount] int NOT NULL DEFAULT (0),
    [LockoutEnabled] bit NOT NULL DEFAULT (0),
    [LockoutEnd] datetimeoffset NULL,
    [LastLogin] datetimeoffset NULL,
    [IsDeleted] bit NOT NULL DEFAULT (0),
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogin]') AND type in (N'U'))
CREATE TABLE [dbo].[UserLogin] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [EmailAddress] nvarchar(256) NOT NULL,
    [UserId] uniqueidentifier NULL,
    [UserAgent] nvarchar(MAX) NULL,
    [Browser] nvarchar(256) NULL,
    [OperatingSystem] nvarchar(256) NULL,
    [DeviceFamily] nvarchar(256) NULL,
    [DeviceBrand] nvarchar(256) NULL,
    [DeviceModel] nvarchar(256) NULL,
    [IpAddress] nvarchar(50) NULL,
    [IsSuccessful] bit NOT NULL DEFAULT (0),
    [FailureMessage] nvarchar(256) NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
CREATE TABLE [dbo].[UserRole] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId])
);


-- Foreign Keys
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Priority_PriorityId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Task]'))
ALTER TABLE [dbo].[Task]
    ADD CONSTRAINT [FK_Task_Priority_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_Status_StatusId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Task]'))
ALTER TABLE [dbo].[Task]
    ADD CONSTRAINT [FK_Task_Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Task_User_AssignedId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Task]'))
ALTER TABLE [dbo].[Task]
    ADD CONSTRAINT [FK_Task_User_AssignedId] FOREIGN KEY ([AssignedId]) REFERENCES [dbo].[User] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TaskExtended_Task_TaskId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TaskExtended]'))
ALTER TABLE [dbo].[TaskExtended]
    ADD CONSTRAINT [FK_TaskExtended_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserLogin_User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserLogin]'))
ALTER TABLE [dbo].[UserLogin]
    ADD CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRole_User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRole]'))
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);


-- Indexes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND name = N'UX_Role_Name')
CREATE UNIQUE INDEX [UX_Role_Name]
ON [dbo].[Role] ([Name]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_AssignedId')
CREATE INDEX [IX_Task_AssignedId]
ON [dbo].[Task] ([AssignedId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_PriorityId')
CREATE INDEX [IX_Task_PriorityId]
ON [dbo].[Task] ([PriorityId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND name = N'IX_Task_StatusId')
CREATE INDEX [IX_Task_StatusId]
ON [dbo].[Task] ([StatusId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND name = N'UX_User_EmailAddress')
CREATE UNIQUE INDEX [UX_User_EmailAddress]
ON [dbo].[User] ([EmailAddress]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserLogin]') AND name = N'IX_UserLogin_EmailAddress')
CREATE INDEX [IX_UserLogin_EmailAddress]
ON [dbo].[UserLogin] ([EmailAddress]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserLogin]') AND name = N'IX_UserLogin_UserId')
CREATE INDEX [IX_UserLogin_UserId]
ON [dbo].[UserLogin] ([UserId]);


-- Table [dbo].[Priority] data

MERGE INTO [dbo].[Priority] AS t
USING 
(
    VALUES
    ('DBF0E04F-04FB-E811-AA64-1E872CB6CB93', 'High', 'High Priority', 1, 1), 
    ('DCF0E04F-04FB-E811-AA64-1E872CB6CB93', 'Normal', 'Normal Priority', 2, 1), 
    ('784C7657-04FB-E811-AA64-1E872CB6CB93', 'Low', 'Low Priority', 3, 1)
) 
AS s
([Id], [Name], [Description], [DisplayOrder], [IsActive])
ON (t.[Id] = s.[Id])
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([Id], [Name], [Description], [DisplayOrder], [IsActive])
    VALUES (s.[Id], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive])
WHEN MATCHED THEN 
    UPDATE SET t.[Name] = s.[Name], t.[Description] = s.[Description], t.[DisplayOrder] = s.[DisplayOrder], t.[IsActive] = s.[IsActive]
OUTPUT $action as [Action];

-- Table [dbo].[Status] data

MERGE INTO [dbo].[Status] AS t
USING 
(
    VALUES
    ('CE002CD8-04FB-E811-AA64-1E872CB6CB93', 'Not Started', 'Not Starated', 1, 1), 
    ('CF002CD8-04FB-E811-AA64-1E872CB6CB93', 'In Progress', 'In Progress', 2, 1), 
    ('D0002CD8-04FB-E811-AA64-1E872CB6CB93', 'Completed', 'Completed', 3, 1), 
    ('D1002CD8-04FB-E811-AA64-1E872CB6CB93', 'Blocked', 'Blocked', 4, 1), 
    ('D2002CD8-04FB-E811-AA64-1E872CB6CB93', 'Deferred', 'Deferred', 5, 1), 
    ('D3002CD8-04FB-E811-AA64-1E872CB6CB93', 'Done', 'Done', 6, 1)
) 
AS s
([Id], [Name], [Description], [DisplayOrder], [IsActive])
ON (t.[Id] = s.[Id])
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([Id], [Name], [Description], [DisplayOrder], [IsActive])
    VALUES (s.[Id], s.[Name], s.[Description], s.[DisplayOrder], s.[IsActive])
WHEN MATCHED THEN 
    UPDATE SET t.[Name] = s.[Name], t.[Description] = s.[Description], t.[DisplayOrder] = s.[DisplayOrder], t.[IsActive] = s.[IsActive]
OUTPUT $action as [Action];

-- Table [dbo].[User] data

MERGE INTO [dbo].[User] AS t
USING 
(
    VALUES
    ('83507c95-0744-e811-bd87-f8633fc30ac7', 'william.adama@battlestar.com', 1, 'William Adama'), 
    ('490312a6-0744-e811-bd87-f8633fc30ac7', 'laura.roslin@battlestar.com', 1, 'Laura Roslin'), 
    ('38da04bb-0744-e811-bd87-f8633fc30ac7', 'kara.thrace@battlestar.com', 1, 'Kara Thrace'), 
    ('589d67c6-0744-e811-bd87-f8633fc30ac7', 'lee.adama@battlestar.com', 1, 'Lee Adama'), 
    ('118b84d4-0744-e811-bd87-f8633fc30ac7', 'gaius.baltar@battlestar.com', 1, 'Gaius Baltar'), 
    ('fa7515df-0744-e811-bd87-f8633fc30ac7', 'saul.tigh@battlestar.com', 1, 'Saul Tigh')
) 
AS s
([Id], [EmailAddress], [IsEmailAddressConfirmed], [DisplayName])
ON (t.[Id] = s.[Id])
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([Id], [EmailAddress], [IsEmailAddressConfirmed], [DisplayName])
    VALUES (s.[Id], s.[EmailAddress], s.[IsEmailAddressConfirmed], s.[DisplayName])
WHEN MATCHED THEN 
    UPDATE SET t.[EmailAddress] = s.[EmailAddress], t.[IsEmailAddressConfirmed] = s.[IsEmailAddressConfirmed], t.[DisplayName] = s.[DisplayName]
OUTPUT $action as [Action];

-- Table [dbo].[Role] data

MERGE INTO [dbo].[Role] AS t
USING 
(
    VALUES
    ('b2d78522-0944-e811-bd87-f8633fc30ac7', 'Administrator', 'Administrator'), 
    ('b3d78522-0944-e811-bd87-f8633fc30ac7', 'Manager', 'Manager'), 
    ('acbffa29-0944-e811-bd87-f8633fc30ac7', 'Member', 'Member')
) 
AS s
([Id], [Name], [Description])
ON (t.[Id] = s.[Id])
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([Id], [Name], [Description])
    VALUES (s.[Id], s.[Name], s.[Description])
WHEN MATCHED THEN 
    UPDATE SET t.[Name] = s.[Name], t.[Description] = s.[Description]
OUTPUT $action as [Action];
