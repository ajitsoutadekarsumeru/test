BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanAccounts]') AND [c].[name] = N'UnAttempted');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [LoanAccounts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [LoanAccounts] DROP COLUMN [UnAttempted];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanAccounts]') AND [c].[name] = N'Paid');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LoanAccounts] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [LoanAccounts] SET [Paid] = CAST(0 AS bit) WHERE [Paid] IS NULL;
ALTER TABLE [LoanAccounts] ALTER COLUMN [Paid] bit NOT NULL;
ALTER TABLE [LoanAccounts] ADD DEFAULT CAST(0 AS bit) FOR [Paid];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanAccounts]') AND [c].[name] = N'Attempted');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [LoanAccounts] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [LoanAccounts] SET [Attempted] = CAST(0 AS bit) WHERE [Attempted] IS NULL;
ALTER TABLE [LoanAccounts] ALTER COLUMN [Attempted] bit NOT NULL;
ALTER TABLE [LoanAccounts] ADD DEFAULT CAST(0 AS bit) FOR [Attempted];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250611042547_UpdatedLoanAccountsFields', N'8.0.10');
GO

COMMIT;
GO

