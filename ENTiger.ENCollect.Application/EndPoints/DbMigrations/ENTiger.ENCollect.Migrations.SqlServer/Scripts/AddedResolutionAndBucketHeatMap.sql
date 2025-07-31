BEGIN TRANSACTION;
GO

DROP TABLE [CommunicationTemplateWorkflowState];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'IsAllowAccessFromAccountDetails');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [CommunicationTemplate] SET [IsAllowAccessFromAccountDetails] = CAST(0 AS bit) WHERE [IsAllowAccessFromAccountDetails] IS NULL;
ALTER TABLE [CommunicationTemplate] ALTER COLUMN [IsAllowAccessFromAccountDetails] bit NOT NULL;
ALTER TABLE [CommunicationTemplate] ADD DEFAULT CAST(0 AS bit) FOR [IsAllowAccessFromAccountDetails];
GO

CREATE TABLE [Resolutions] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [Description] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Resolutions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BucketHeatMapConfig] (
    [Id] nvarchar(32) NOT NULL,
    [BucketId] nvarchar(32) NULL,
    [Resolutions] nvarchar(32) NULL,
    [ResolutionMasterId] nvarchar(32) NULL,
    [RangeFrom] int NOT NULL,
    [RangeTo] int NOT NULL,
    [Color] nvarchar(30) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BucketHeatMapConfig] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BucketHeatMapConfig_Buckets_BucketId] FOREIGN KEY ([BucketId]) REFERENCES [Buckets] ([Id]),
    CONSTRAINT [FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId] FOREIGN KEY ([ResolutionMasterId]) REFERENCES [Resolutions] ([Id])
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-17T15:49:13.2549657+05:30', [LastModifiedDate] = '2025-06-17T15:49:13.2549702+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-17T15:49:13.2549717+05:30', [LastModifiedDate] = '2025-06-17T15:49:13.2549718+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-17T15:49:13.2549711+05:30', [LastModifiedDate] = '2025-06-17T15:49:13.2549712+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-17T15:49:13.2549727+05:30', [LastModifiedDate] = '2025-06-17T15:49:13.2549728+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_BucketHeatMapConfig_BucketId] ON [BucketHeatMapConfig] ([BucketId]);
GO

CREATE INDEX [IX_BucketHeatMapConfig_ResolutionMasterId] ON [BucketHeatMapConfig] ([ResolutionMasterId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250617101917_AddedResolutionAndBucketHeatMap', N'8.0.10');
GO

COMMIT;
GO

