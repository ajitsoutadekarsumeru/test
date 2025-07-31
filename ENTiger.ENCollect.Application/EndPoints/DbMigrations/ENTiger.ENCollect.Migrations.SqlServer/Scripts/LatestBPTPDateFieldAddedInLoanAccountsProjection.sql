BEGIN TRANSACTION;
GO

ALTER TABLE [LoanAccountsProjection] ADD [LatestBPTPDate] datetimeoffset NULL;
GO


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250610100246_LatestBPTPDateFieldAddedInLoanAccountsProjection', N'8.0.10');
GO

COMMIT;
GO

