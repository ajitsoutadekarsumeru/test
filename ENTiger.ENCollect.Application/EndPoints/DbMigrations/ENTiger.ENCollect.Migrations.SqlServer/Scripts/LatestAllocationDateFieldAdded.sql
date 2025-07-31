BEGIN TRANSACTION;
GO


ALTER TABLE [LoanAccounts] ADD [LatestAllocationDate] datetime2 NULL;
GO


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250403112325_LatestAllocationDateFieldAdded', N'8.0.10');
GO

COMMIT;
GO

