BEGIN TRANSACTION;
GO

DROP TABLE [BucketHeatMapConfig];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Resolutions]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Resolutions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Resolutions] DROP COLUMN [Name];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Address]') AND [c].[name] = N'AddressLine1');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Address] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Address] DROP COLUMN [AddressLine1];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Address]') AND [c].[name] = N'AddressLine2');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Address] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Address] DROP COLUMN [AddressLine2];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Address]') AND [c].[name] = N'AddressLine3');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Address] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Address] DROP COLUMN [AddressLine3];
GO

ALTER TABLE [Resolutions] ADD [Code] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Buckets] ADD [DPD_From] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Buckets] ADD [DPD_To] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Buckets] ADD [DisplayLabel] nvarchar(50) NULL;
GO

ALTER TABLE [Address] ADD [AddressLine] nvarchar(200) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountGeoMap]') AND [c].[name] = N'HierarchyLevel');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AccountGeoMap] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [AccountGeoMap] SET [HierarchyLevel] = 0 WHERE [HierarchyLevel] IS NULL;
ALTER TABLE [AccountGeoMap] ALTER COLUMN [HierarchyLevel] int NOT NULL;
ALTER TABLE [AccountGeoMap] ADD DEFAULT 0 FOR [HierarchyLevel];
GO

CREATE TABLE [AccountProductMap] (
    [Id] nvarchar(32) NOT NULL,
    [AccountId] nvarchar(32) NOT NULL,
    [HierarchyId] nvarchar(32) NOT NULL,
    [HierarchyLevel] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountProductMap] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountProductMap_HierarchyMaster_HierarchyId] FOREIGN KEY ([HierarchyId]) REFERENCES [HierarchyMaster] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountProductMap_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [BranchGeoMap] (
    [Id] nvarchar(32) NOT NULL,
    [BranchId] nvarchar(32) NOT NULL,
    [HierarchyId] nvarchar(32) NOT NULL,
    [HierarchyLevel] int NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BranchGeoMap] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BranchGeoMap_ApplicationOrg_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [ApplicationOrg] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BranchGeoMap_HierarchyMaster_HierarchyId] FOREIGN KEY ([HierarchyId]) REFERENCES [HierarchyMaster] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [HeatMapConfig] (
    [Id] nvarchar(32) NOT NULL,
    [HeatMapType] nvarchar(100) NULL,
    [RowId] nvarchar(32) NULL,
    [ColumnId] nvarchar(32) NULL,
    [RangeFrom] int NOT NULL,
    [RangeTo] int NOT NULL,
    [HeatIndicator] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_HeatMapConfig] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ReportDownloadLog] (
    [Id] nvarchar(32) NOT NULL,
    [ReportType] nvarchar(200) NULL,
    [ReportFilters] nvarchar(max) NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ReportDownloadLog] PRIMARY KEY ([Id])
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-11T12:22:57.6270551+05:30', [LastModifiedDate] = '2025-07-11T12:22:57.6270591+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-11T12:22:57.6270602+05:30', [LastModifiedDate] = '2025-07-11T12:22:57.6270603+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-11T12:22:57.6270597+05:30', [LastModifiedDate] = '2025-07-11T12:22:57.6270598+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-11T12:22:57.6270612+05:30', [LastModifiedDate] = '2025-07-11T12:22:57.6270613+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_AccountProductMap_AccountId] ON [AccountProductMap] ([AccountId]);
GO

CREATE INDEX [IX_AccountProductMap_HierarchyId] ON [AccountProductMap] ([HierarchyId]);
GO

CREATE INDEX [IX_BranchGeoMap_BranchId] ON [BranchGeoMap] ([BranchId]);
GO

CREATE INDEX [IX_BranchGeoMap_HierarchyId] ON [BranchGeoMap] ([HierarchyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250711065302_ReportsDownloadLogTableAdded', N'8.0.10');
GO

COMMIT;
GO

