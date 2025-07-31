BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'IsActive');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Permissions] DROP COLUMN [IsActive];
GO

EXEC sp_rename N'[Permissions].[PermissionName]', N'Name', N'COLUMN';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Permissions] ALTER COLUMN [Description] nvarchar(255) NULL;
GO

ALTER TABLE [Permissions] ADD [Section] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Designation] ADD [PermissionSchemeId] nvarchar(32) NULL;
GO

ALTER TABLE [ApplicationUser] ADD [UserType] nvarchar(20) NOT NULL DEFAULT N'';
GO

CREATE TABLE [PermissionSchemeChangeLog] (
    [Id] nvarchar(32) NOT NULL,
    [PermissionSchemeId] nvarchar(max) NULL,
    [AddedPermissions] nvarchar(max) NULL,
    [RemovedPermissions] nvarchar(max) NULL,
    [ChangeType] nvarchar(max) NULL,
    [Remarks] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PermissionSchemeChangeLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PermissionSchemes] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PermissionSchemes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [EnabledPermission] (
    [Id] nvarchar(32) NOT NULL,
    [PermissionId] nvarchar(32) NOT NULL,
    [PermissionSchemeId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EnabledPermission] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EnabledPermission_PermissionSchemes_PermissionSchemeId] FOREIGN KEY ([PermissionSchemeId]) REFERENCES [PermissionSchemes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EnabledPermission_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771589+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771682+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771712+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771714+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771703+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771705+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771752+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771754+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Designation_PermissionSchemeId] ON [Designation] ([PermissionSchemeId]);
GO

CREATE INDEX [IX_EnabledPermission_PermissionId] ON [EnabledPermission] ([PermissionId]);
GO

CREATE INDEX [IX_EnabledPermission_PermissionSchemeId] ON [EnabledPermission] ([PermissionSchemeId]);
GO

ALTER TABLE [Designation] ADD CONSTRAINT [FK_Designation_PermissionSchemes_PermissionSchemeId] FOREIGN KEY ([PermissionSchemeId]) REFERENCES [PermissionSchemes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513050356_AddedPermissionScheme', N'8.0.10');
GO

COMMIT;
GO

