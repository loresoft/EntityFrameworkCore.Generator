-- Tables
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit]') AND type in (N'U'))
CREATE TABLE [dbo].[Audit] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime NOT NULL,
    [UserId] int NULL,
    [TaskId] int NULL,
    [Content] nvarchar(MAX) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Priority]') AND type in (N'U'))
CREATE TABLE [dbo].[Priority] (
    [Id] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [DisplayOrder] int NOT NULL DEFAULT (0),
    [IsActive] bit NOT NULL DEFAULT (1),
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Priority] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
CREATE TABLE [dbo].[Role] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Description] nvarchar(MAX) NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
CREATE TABLE [dbo].[Status] (
    [Id] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [DisplayOrder] int NOT NULL DEFAULT (0),
    [IsActive] bit NOT NULL DEFAULT (1),
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
CREATE TABLE [dbo].[Task] (
    [Id] uniqueidentifier NOT NULL,
    [StatusId] int NOT NULL,
    [PriorityId] int NULL,
    [Title] nvarchar(255) NOT NULL,
    [Description] nvarchar(MAX) NULL,
    [StartDate] datetimeoffset NULL,
    [DueDate] datetimeoffset NULL,
    [CompleteDate] datetimeoffset NULL,
    [AssignedId] uniqueidentifier NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
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
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_TaskExtended] PRIMARY KEY ([TaskId])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
CREATE TABLE [dbo].[User] (
    [Id] uniqueidentifier NOT NULL,
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
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [UpdatedBy] nvarchar(100) NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogin]') AND type in (N'U'))
CREATE TABLE [dbo].[UserLogin] (
    [Id] uniqueidentifier NOT NULL,
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
    [Created] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
    [CreatedBy] nvarchar(100) NULL,
    [Updated] datetimeoffset NOT NULL DEFAULT (sysutcdatetime()),
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

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CitiesSpatial]') AND type in (N'U'))
CREATE TABLE [dbo].[CitiesSpatial](
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NULL,
    [GeometryField] [geometry] NOT NULL,
    [GeographyField] [geography] NOT NULL,
 CONSTRAINT [PK_CitiesSpatial] PRIMARY KEY ([Id])
) 

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


