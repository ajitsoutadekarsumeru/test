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

CREATE TABLE [AccountContactHistory] (
    [Id] nvarchar(32) NOT NULL,
    [ContactValue] nvarchar(200) NOT NULL,
    [Latitude] decimal(9,6) NULL,
    [Longitude] decimal(9,6) NULL,
    [ContactSource] int NOT NULL,
    [ContactType] int NOT NULL,
    [AccountId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountContactHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountContactHistory_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id])
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




CREATE INDEX [IX_AccountContactHistory_AccountId] ON [AccountContactHistory] ([AccountId]);
GO

CREATE INDEX [IX_BranchGeoMap_BranchId] ON [BranchGeoMap] ([BranchId]);
GO

CREATE INDEX [IX_BranchGeoMap_HierarchyId] ON [BranchGeoMap] ([HierarchyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250714102602_AddTableAccountContactHistory', N'8.0.10');
GO

COMMIT;
GO

