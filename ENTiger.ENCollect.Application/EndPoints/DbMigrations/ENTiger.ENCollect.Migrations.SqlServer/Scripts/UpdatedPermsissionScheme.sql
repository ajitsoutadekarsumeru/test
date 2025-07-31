BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'RemovedPermissions');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [RemovedPermissions] nvarchar(2000) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'Remarks');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [PermissionSchemeChangeLog] SET [Remarks] = N'' WHERE [Remarks] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [Remarks] nvarchar(500) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [Remarks];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'PermissionSchemeId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [PermissionSchemeChangeLog] SET [PermissionSchemeId] = N'' WHERE [PermissionSchemeId] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [PermissionSchemeId] nvarchar(32) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [PermissionSchemeId];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'ChangeType');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [PermissionSchemeChangeLog] SET [ChangeType] = N'' WHERE [ChangeType] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [ChangeType] nvarchar(50) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [ChangeType];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'AddedPermissions');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [AddedPermissions] nvarchar(2000) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'Name');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var5 + '];');
UPDATE [Permissions] SET [Name] = N'' WHERE [Name] IS NULL;
ALTER TABLE [Permissions] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
ALTER TABLE [Permissions] ADD DEFAULT N'' FOR [Name];
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606003+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606034+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606051+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606052+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606044+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606045+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606065+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606066+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514042027_UpdatedPermsissionScheme', N'8.0.10');
GO

COMMIT;
GO

