BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ApplicationUser]') AND [c].[name] = N'MaxHotleadCount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ApplicationUser] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ApplicationUser] DROP COLUMN [MaxHotleadCount];
GO

ALTER TABLE [ApplicationUser] ADD [GeoLevelId] nvarchar(32) NULL;
GO

ALTER TABLE [ApplicationUser] ADD [ProductLevelId] nvarchar(32) NULL;
GO

CREATE TABLE [UserBucketScope] (
    [Id] nvarchar(32) NOT NULL,
    [BucketScopeId] nvarchar(32) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserBucketScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserBucketScope_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_UserBucketScope_Buckets_BucketScopeId] FOREIGN KEY ([BucketScopeId]) REFERENCES [Buckets] ([Id])
);
GO

CREATE TABLE [UserGeoScope] (
    [Id] nvarchar(32) NOT NULL,
    [GeoScopeId] nvarchar(32) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserGeoScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserGeoScope_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_UserGeoScope_HierarchyMaster_GeoScopeId] FOREIGN KEY ([GeoScopeId]) REFERENCES [HierarchyMaster] ([Id])
);
GO

CREATE TABLE [UserProductScope] (
    [Id] nvarchar(32) NOT NULL,
    [ProductScopeId] nvarchar(32) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserProductScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserProductScope_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_UserProductScope_HierarchyMaster_ProductScopeId] FOREIGN KEY ([ProductScopeId]) REFERENCES [HierarchyMaster] ([Id])
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-19T17:45:15.0131644+05:30', [LastModifiedDate] = '2025-06-19T17:45:15.0131718+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-19T17:45:15.0131755+05:30', [LastModifiedDate] = '2025-06-19T17:45:15.0131758+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-19T17:45:15.0131741+05:30', [LastModifiedDate] = '2025-06-19T17:45:15.0131744+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-19T17:45:15.0131791+05:30', [LastModifiedDate] = '2025-06-19T17:45:15.0131794+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ApplicationUser_GeoLevelId] ON [ApplicationUser] ([GeoLevelId]);
GO

CREATE INDEX [IX_ApplicationUser_ProductLevelId] ON [ApplicationUser] ([ProductLevelId]);
GO

CREATE INDEX [IX_UserBucketScope_ApplicationUserId] ON [UserBucketScope] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserBucketScope_BucketScopeId] ON [UserBucketScope] ([BucketScopeId]);
GO

CREATE INDEX [IX_UserGeoScope_ApplicationUserId] ON [UserGeoScope] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserGeoScope_GeoScopeId] ON [UserGeoScope] ([GeoScopeId]);
GO

CREATE INDEX [IX_UserProductScope_ApplicationUserId] ON [UserProductScope] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserProductScope_ProductScopeId] ON [UserProductScope] ([ProductScopeId]);
GO

ALTER TABLE [ApplicationUser] ADD CONSTRAINT [FK_ApplicationUser_HierarchyLevel_GeoLevelId] FOREIGN KEY ([GeoLevelId]) REFERENCES [HierarchyLevel] ([Id]);
GO

ALTER TABLE [ApplicationUser] ADD CONSTRAINT [FK_ApplicationUser_HierarchyLevel_ProductLevelId] FOREIGN KEY ([ProductLevelId]) REFERENCES [HierarchyLevel] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250619121518_AddedSOW', N'8.0.10');
GO

COMMIT;
GO

