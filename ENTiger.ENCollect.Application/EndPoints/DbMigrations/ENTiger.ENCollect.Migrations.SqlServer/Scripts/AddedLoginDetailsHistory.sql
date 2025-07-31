BEGIN TRANSACTION;
GO

ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGroupMasterId];
GO

ALTER TABLE [State] DROP CONSTRAINT [FK_State_Regions_RegionId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GeoMaster]') AND [c].[name] = N'Zone');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [GeoMaster] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [GeoMaster] DROP COLUMN [Zone];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'SecondaryLanguage');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [State] ALTER COLUMN [SecondaryLanguage] nvarchar(100) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'RegionId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [State] ALTER COLUMN [RegionId] nvarchar(32) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'PrimaryLanguage');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [State] ALTER COLUMN [PrimaryLanguage] nvarchar(100) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'NickName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [State] ALTER COLUMN [NickName] nvarchar(50) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'Name');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [State] ALTER COLUMN [Name] nvarchar(max) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [LastUploadedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feedback]') AND [c].[name] = N'AssignTo');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Feedback] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Feedback] ALTER COLUMN [AssignTo] nvarchar(50) NULL;
GO

ALTER TABLE [Feedback] ADD [Place_of_visit] nvarchar(500) NULL;
GO

ALTER TABLE [Feedback] ADD [ThirdPartyContactNo] nvarchar(50) NULL;
GO

ALTER TABLE [Feedback] ADD [ThirdPartyContactPerson] nvarchar(50) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'ShortDescription');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [ShortDescription] nvarchar(max) NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'Permissibleforfieldagent');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [Permissibleforfieldagent] nvarchar(max) NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'LongDescription');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [LongDescription] nvarchar(max) NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'DispositionGroupMasterId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [DispositionGroupMasterId] nvarchar(32) NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'DispositionCode');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [DispositionCode] nvarchar(max) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'DispositionAccess');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [DispositionCodeMaster] ALTER COLUMN [DispositionAccess] nvarchar(150) NULL;
GO

ALTER TABLE [ApplicationUser] ADD [Age] int NULL;
GO

ALTER TABLE [ApplicationUser] ADD [DRAStatus] nvarchar(20) NULL;
GO

CREATE TABLE [DailyActivityDetail] (
    [Id] nvarchar(32) NOT NULL,
    [ActivityTs] datetime2 NULL,
    [ActivityType] nvarchar(100) NULL,
    [Location] nvarchar(300) NULL,
    [ActivityWeekDay] nvarchar(50) NULL,
    [ActivityDayNumber] int NULL,
    [ActivityMonth] int NULL,
    [ActivityYear] int NULL,
    [State] nvarchar(250) NULL,
    [Lat] float NULL,
    [Department] nvarchar(250) NULL,
    [Mobile] nvarchar(50) NULL,
    [Name] nvarchar(200) NULL,
    [StaffOrAgent] bit NULL,
    [EmpanalmentStatus] nvarchar(250) NULL,
    [Designation] nvarchar(250) NULL,
    [Agency] nvarchar(200) NULL,
    [Branch] nvarchar(200) NULL,
    [UserId] nvarchar(200) NULL,
    [EmailId] nvarchar(100) NULL,
    [Long] float NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DailyActivityDetail] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [LoginDetailsHistory] (
    [Id] nvarchar(32) NOT NULL,
    [UserId] nvarchar(32) NULL,
    [Email] nvarchar(200) NULL,
    [LoginStatus] nvarchar(500) NULL,
    [LoginInputJson] nvarchar(2000) NULL,
    [Remarks] nvarchar(1000) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoginDetailsHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoginDetailsHistory_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [TrailGapDownload] (
    [Id] nvarchar(32) NOT NULL,
    [InputJson] nvarchar(1000) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(500) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TrailGapDownload] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TrailIntensityDownload] (
    [Id] nvarchar(32) NOT NULL,
    [InputJson] nvarchar(1000) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(500) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TrailIntensityDownload] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_LoginDetailsHistory_UserId] ON [LoginDetailsHistory] ([UserId]);
GO

ALTER TABLE [DispositionCodeMaster] ADD CONSTRAINT [FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGroupMasterId] FOREIGN KEY ([DispositionGroupMasterId]) REFERENCES [DispositionGroupMaster] ([Id]);
GO

ALTER TABLE [State] ADD CONSTRAINT [FK_State_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240926091305_AddedLoginDetailsHistory', N'8.0.6');
GO

COMMIT;
GO

