BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanAccounts]') AND [c].[name] = N'LastXDigitsOfAgreementId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [LoanAccounts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [LoanAccounts] DROP COLUMN [LastXDigitsOfAgreementId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanAccounts]') AND [c].[name] = N'LastXDigitsOfPrimaryCard');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LoanAccounts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [LoanAccounts] DROP COLUMN [LastXDigitsOfPrimaryCard];
GO

ALTER TABLE [LoanAccounts] ADD [ReverseOfAgreementId] AS REVERSE(AgreementId) PERSISTED;
GO

ALTER TABLE [LoanAccounts] ADD [ReverseOfPrimaryCard] AS REVERSE(PRIMARY_CARD_NUMBER) PERSISTED;
GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:25:06.9984127+02:00', [LastModifiedDate] = '2025-03-25T09:25:06.9984164+02:00'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:25:06.9984176+02:00', [LastModifiedDate] = '2025-03-25T09:25:06.9984178+02:00'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:25:06.9984170+02:00', [LastModifiedDate] = '2025-03-25T09:25:06.9984171+02:00'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:25:06.9984187+02:00', [LastModifiedDate] = '2025-03-25T09:25:06.9984188+02:00'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_LoanAccounts_ReverseOfAgreementId] ON [LoanAccounts] ([ReverseOfAgreementId]);
GO

CREATE INDEX [IX_LoanAccounts_ReverseOfPrimaryCard] ON [LoanAccounts] ([ReverseOfPrimaryCard]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250325072510_AddedLastXDigitsOfAccountSearch', N'8.0.10');
GO

COMMIT;
GO

