BEGIN TRANSACTION;
GO

ALTER TABLE [LoanAccounts] ADD [LastXDigitsOfAgreementId] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [LastXDigitsOfPrimaryCard] nvarchar(25) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250213102444_AddedLastXDigitsOfAccountNoAndPrimaryCard', N'8.0.10');
GO

COMMIT;
GO

