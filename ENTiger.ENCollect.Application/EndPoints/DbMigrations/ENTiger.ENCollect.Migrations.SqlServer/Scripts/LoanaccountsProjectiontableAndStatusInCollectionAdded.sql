BEGIN TRANSACTION;
GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Settlement] ADD [CURRENT_BUCKET] nvarchar(50) NULL;
GO

ALTER TABLE [Settlement] ADD [CURRENT_DPD] bigint NULL;
GO

ALTER TABLE [Settlement] ADD [ChargesOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [CustomId] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [DateOfDeath] datetime2 NULL;
GO

ALTER TABLE [Settlement] ADD [InterestOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [IsDeathSettlement] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Settlement] ADD [LatestHistoryId] nvarchar(32) NULL;
GO

ALTER TABLE [Settlement] ADD [LoanAccountId] nvarchar(32) NULL;
GO

ALTER TABLE [Settlement] ADD [NPA_STAGEID] nvarchar(50) NULL;
GO

ALTER TABLE [Settlement] ADD [NumberOfEmisDue] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Settlement] ADD [NumberOfInstallments] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Settlement] ADD [PrincipalOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [SettlementAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [SettlementDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [SettlementDateForDuesCalc] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [SettlementRemarks] nvarchar(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [Status] nvarchar(100) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [StatusUpdatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [TOS] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [TrancheType] nvarchar(50) NOT NULL DEFAULT N'';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Designation]') AND [c].[name] = N'Level');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Designation] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [Designation] SET [Level] = 0 WHERE [Level] IS NULL;
ALTER TABLE [Designation] ALTER COLUMN [Level] int NOT NULL;
ALTER TABLE [Designation] ADD DEFAULT 0 FOR [Level];
GO

ALTER TABLE [Collections] ADD [Status] nvarchar(50) NULL;
GO

CREATE TABLE [InstallmentDetail] (
    [Id] nvarchar(32) NOT NULL,
    [InstallmentAmount] decimal(18,2) NOT NULL,
    [InstallmentDueDate] datetime2 NOT NULL,
    [SettlementId] nvarchar(32) NULL,
    CONSTRAINT [PK_InstallmentDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InstallmentDetail_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id])
);
GO

CREATE TABLE [LevelDesignation] (
    [Id] nvarchar(32) NOT NULL,
    [Level] int NOT NULL,
    [DesignationId] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_LevelDesignation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LevelDesignation_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LoanAccountsProjection] (
    [Id] nvarchar(32) NOT NULL,
    [Year] int NULL,
    [Month] int NULL,
    [LoanAccountId] nvarchar(32) NOT NULL,
    [TotalCollectionAmount] decimal(18,2) NULL,
    [TotalCollectionCount] int NULL,
    [LastCollectionDate] datetime2 NULL,
    [TotalTrailCount] int NULL,
    [TotalPTPCount] int NULL,
    [TotalBPTPCount] int NULL,
    [CurrentDispositionGroup] nvarchar(50) NULL,
    [CurrentDispositionCode] nvarchar(50) NULL,
    [CurrentDispositionDate] datetime2 NULL,
    [CurrentNextActionDate] datetime2 NULL,
    [PreviousDispositionGroup] nvarchar(50) NULL,
    [PreviousDispositionCode] nvarchar(50) NULL,
    [PreviousDispositionDate] datetime2 NULL,
    [PreviousNextActionDate] datetime2 NULL,
    [Version] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoanAccountsProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccountsProjection_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SettlementDocument] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [DocumentType] nvarchar(50) NOT NULL,
    [DocumentName] nvarchar(50) NOT NULL,
    [FileName] nvarchar(100) NOT NULL,
    [UploadedOn] datetimeoffset NOT NULL,
    CONSTRAINT [PK_SettlementDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementDocument_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SettlementQueueProjection] (
    [Id] nvarchar(32) NOT NULL,
    [WorkflowName] nvarchar(50) NOT NULL,
    [WorkflowInstanceId] nvarchar(50) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [StepIndex] int NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SettlementQueueProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementQueueProjection_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SettlementQueueProjection_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SettlementStatusHistory] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [FromStatus] nvarchar(50) NOT NULL,
    [ToStatus] nvarchar(50) NOT NULL,
    [ChangedByUserId] nvarchar(50) NOT NULL,
    [ChangedBy] nvarchar(32) NULL,
    [ChangedDate] datetimeoffset NOT NULL,
    [Comment] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_SettlementStatusHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementStatusHistory_ApplicationUser_ChangedBy] FOREIGN KEY ([ChangedBy]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_SettlementStatusHistory_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLevelProjection] (
    [Id] nvarchar(32) NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    [Level] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserLevelProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserLevelProjection_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [WaiverDetail] (
    [Id] nvarchar(32) NOT NULL,
    [ChargeType] nvarchar(50) NOT NULL,
    [AmountAsPerCBS] decimal(18,2) NOT NULL,
    [ApportionmentAmount] decimal(18,2) NOT NULL,
    [WaiverAmount] decimal(18,2) NOT NULL,
    [WaiverPercent] decimal(18,2) NOT NULL,
    [SettlementId] nvarchar(32) NULL,
    CONSTRAINT [PK_WaiverDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WaiverDetail_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id])
);
GO

CREATE TABLE [WorkflowAssignment] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementStatusHistoryId] nvarchar(32) NOT NULL,
    [LevelDesignationId] nvarchar(50) NULL,
    CONSTRAINT [PK_WorkflowAssignment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkflowAssignment_LevelDesignation_LevelDesignationId] FOREIGN KEY ([LevelDesignationId]) REFERENCES [LevelDesignation] ([Id]),
    CONSTRAINT [FK_WorkflowAssignment_SettlementStatusHistory_SettlementStatusHistoryId] FOREIGN KEY ([SettlementStatusHistoryId]) REFERENCES [SettlementStatusHistory] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AssignedUser] (
    [Id] nvarchar(32) NOT NULL,
    [WorkflowAssignmentId] nvarchar(32) NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_AssignedUser] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AssignedUser_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssignedUser_WorkflowAssignment_WorkflowAssignmentId] FOREIGN KEY ([WorkflowAssignmentId]) REFERENCES [WorkflowAssignment] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Settlement_LatestHistoryId] ON [Settlement] ([LatestHistoryId]);
GO

CREATE INDEX [IX_Settlement_LoanAccountId] ON [Settlement] ([LoanAccountId]);
GO

CREATE INDEX [IX_AssignedUser_ApplicationUserId] ON [AssignedUser] ([ApplicationUserId]);
GO

CREATE INDEX [IX_AssignedUser_WorkflowAssignmentId] ON [AssignedUser] ([WorkflowAssignmentId]);
GO

CREATE INDEX [IX_InstallmentDetail_SettlementId] ON [InstallmentDetail] ([SettlementId]);
GO

CREATE INDEX [IX_LevelDesignation_DesignationId] ON [LevelDesignation] ([DesignationId]);
GO

CREATE INDEX [IX_LoanAccountsProjection_LoanAccountId] ON [LoanAccountsProjection] ([LoanAccountId]);
GO

CREATE INDEX [IX_SettlementDocument_SettlementId] ON [SettlementDocument] ([SettlementId]);
GO

CREATE INDEX [IX_SettlementQueueProjection_ApplicationUserId] ON [SettlementQueueProjection] ([ApplicationUserId]);
GO

CREATE INDEX [IX_SettlementQueueProjection_SettlementId] ON [SettlementQueueProjection] ([SettlementId]);
GO

CREATE INDEX [IX_SettlementStatusHistory_ChangedBy] ON [SettlementStatusHistory] ([ChangedBy]);
GO

CREATE INDEX [IX_SettlementStatusHistory_SettlementId] ON [SettlementStatusHistory] ([SettlementId]);
GO

CREATE INDEX [IX_UserLevelProjection_ApplicationUserId] ON [UserLevelProjection] ([ApplicationUserId]);
GO

CREATE INDEX [IX_WaiverDetail_SettlementId] ON [WaiverDetail] ([SettlementId]);
GO

CREATE INDEX [IX_WorkflowAssignment_LevelDesignationId] ON [WorkflowAssignment] ([LevelDesignationId]);
GO

CREATE INDEX [IX_WorkflowAssignment_SettlementStatusHistoryId] ON [WorkflowAssignment] ([SettlementStatusHistoryId]);
GO

ALTER TABLE [Settlement] ADD CONSTRAINT [FK_Settlement_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]);
GO

ALTER TABLE [Settlement] ADD CONSTRAINT [FK_Settlement_SettlementStatusHistory_LatestHistoryId] FOREIGN KEY ([LatestHistoryId]) REFERENCES [SettlementStatusHistory] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250523133127_LoanaccountsProjectiontableAndStatusInCollectionAdded', N'8.0.10');
GO

COMMIT;
GO

