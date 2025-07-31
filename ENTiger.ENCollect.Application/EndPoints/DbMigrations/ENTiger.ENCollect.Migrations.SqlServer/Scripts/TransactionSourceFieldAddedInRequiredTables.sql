BEGIN TRANSACTION;
GO

ALTER TABLE [UserAttendanceLog] ADD [TransactionSource] nvarchar(20) NULL;
GO

ALTER TABLE [PayInSlips] ADD [TransactionSource] nvarchar(20) NULL;
GO

ALTER TABLE [Feedback] ADD [TransactionSource] nvarchar(20) NULL;
GO

ALTER TABLE [DispositionCodeMaster] ADD [DispositionCodeCustomerOrAccountLevel] nvarchar(50) NULL;
GO

ALTER TABLE [Collections] ADD [TransactionSource] nvarchar(20) NULL;
GO

UPDATE [AgencyType] SET [SubType] = N'field agents'
WHERE [Id] = N'ff379ce22f7b4aca9e74d0dadccb3739';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250224111922_TransactionSourceFieldAdded', N'8.0.10');
GO

COMMIT;
GO

