BEGIN TRANSACTION;
GO

ALTER TABLE [ApplicationUser] ADD [TransactionSource] nvarchar(20) NULL;
GO

ALTER TABLE [ApplicationOrg] ADD [TransactionSource] nvarchar(20) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250318085027_AddedTransactionSourceInApplicationUser', N'8.0.10');
GO

COMMIT;
GO

