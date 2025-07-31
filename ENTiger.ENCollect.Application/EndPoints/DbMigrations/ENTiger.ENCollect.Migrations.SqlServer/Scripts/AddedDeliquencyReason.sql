BEGIN TRANSACTION;
GO

DROP INDEX [IX_LoanAccountJSON_AccountId] ON [LoanAccountJSON];
GO

ALTER TABLE [Feedback] ADD [DeliquencyReason] nvarchar(50) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyUserIdentification]') AND [c].[name] = N'Value');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AgencyUserIdentification] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AgencyUserIdentification] ALTER COLUMN [Value] nvarchar(500) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyUserIdentification]') AND [c].[name] = N'Status');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AgencyUserIdentification] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AgencyUserIdentification] ALTER COLUMN [Status] nvarchar(50) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyUserIdentification]') AND [c].[name] = N'Remarks');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AgencyUserIdentification] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AgencyUserIdentification] ALTER COLUMN [Remarks] nvarchar(200) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyIdentification]') AND [c].[name] = N'Value');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AgencyIdentification] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AgencyIdentification] ALTER COLUMN [Value] nvarchar(500) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyIdentification]') AND [c].[name] = N'Status');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AgencyIdentification] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [AgencyIdentification] ALTER COLUMN [Status] nvarchar(50) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyIdentification]') AND [c].[name] = N'Remarks');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AgencyIdentification] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [AgencyIdentification] ALTER COLUMN [Remarks] nvarchar(200) NULL;
GO

CREATE UNIQUE INDEX [IX_LoanAccountJSON_AccountId] ON [LoanAccountJSON] ([AccountId]) WHERE [AccountId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241111092723_AddedDeliquencyReason', N'8.0.6');
GO

COMMIT;
GO

