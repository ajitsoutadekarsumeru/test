BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Collections]') AND [c].[name] = N'yPANNo');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Collections] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Collections] ALTER COLUMN [yPANNo] nvarchar(200) NULL;
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-15T11:27:23.5336084+05:30', [LastModifiedDate] = '2025-07-15T11:27:23.5336112+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-15T11:27:23.5336128+05:30', [LastModifiedDate] = '2025-07-15T11:27:23.5336129+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-15T11:27:23.5336122+05:30', [LastModifiedDate] = '2025-07-15T11:27:23.5336123+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-15T11:27:23.5336139+05:30', [LastModifiedDate] = '2025-07-15T11:27:23.5336140+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250715055728_UpdatedLengthOfCollectionPanNo', N'8.0.10');
GO

COMMIT;
GO

