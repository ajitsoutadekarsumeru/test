BEGIN TRANSACTION;
GO

ALTER TABLE [ReceiptWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [PayInSlipWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [CompanyUserWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [CommunicationTemplateWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [CollectionWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [CollectionBatchWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [AgencyWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

ALTER TABLE [AgencyUserWorkflowState] ADD [Remarks] nvarchar(500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241004111317_RemarksFieldAddedInWorkflowState', N'8.0.6');
GO

COMMIT;
GO

