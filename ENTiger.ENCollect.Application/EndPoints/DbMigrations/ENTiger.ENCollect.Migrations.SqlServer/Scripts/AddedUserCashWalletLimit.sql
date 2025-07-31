BEGIN TRANSACTION;
GO

ALTER TABLE [ApplicationUser] ADD [UsedWallet] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [ApplicationUser] ADD [WalletLastUpdatedDate] datetime2 NULL;
GO

ALTER TABLE [ApplicationUser] ADD [WalletLimit] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250225141307_AddedUserCashWalletLimit', N'8.0.10');
GO

COMMIT;
GO

