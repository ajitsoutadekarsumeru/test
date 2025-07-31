BEGIN TRANSACTION;
GO

ALTER TABLE [LoanAccounts] ADD [Latest_Address_From_Trail] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [Latest_Email_From_Trail] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [Latest_Number_From_Receipt] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [Latest_Number_From_Send_Payment] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [Latest_Number_From_Trail] nvarchar(50) NULL;
GO

ALTER TABLE [LoanAccounts] ADD [SegmentId] nvarchar(32) NULL;
GO

CREATE INDEX [IX_LoanAccounts_SegmentId] ON [LoanAccounts] ([SegmentId]);
GO

ALTER TABLE [LoanAccounts] ADD CONSTRAINT [FK_LoanAccounts_Segmentation_SegmentId] FOREIGN KEY ([SegmentId]) REFERENCES [Segmentation] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241015115845_AddedAccountContactDetails', N'8.0.6');
GO

COMMIT;
GO

