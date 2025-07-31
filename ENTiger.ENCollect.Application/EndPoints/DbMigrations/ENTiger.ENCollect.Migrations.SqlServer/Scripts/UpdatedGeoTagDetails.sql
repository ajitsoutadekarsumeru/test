BEGIN TRANSACTION;
GO

ALTER TABLE [SettlementStatusHistory] DROP CONSTRAINT [FK_SettlementStatusHistory_ApplicationUser_ChangedBy];
GO

DROP TABLE [AssignedUser];
GO

DROP TABLE [WorkflowAssignment];
GO

DROP TABLE [LevelDesignation];
GO

DROP INDEX [IX_SettlementStatusHistory_ChangedBy] ON [SettlementStatusHistory];
GO

DROP INDEX [IX_LoanAccountsProjection_LoanAccountId] ON [LoanAccountsProjection];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SettlementStatusHistory]') AND [c].[name] = N'ChangedBy');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SettlementStatusHistory] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SettlementStatusHistory] DROP COLUMN [ChangedBy];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SettlementQueueProjection]') AND [c].[name] = N'WorkflowInstanceId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SettlementQueueProjection] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [SettlementQueueProjection] ALTER COLUMN [WorkflowInstanceId] nvarchar(150) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SettlementDocument]') AND [c].[name] = N'FileName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SettlementDocument] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [SettlementDocument] ALTER COLUMN [FileName] nvarchar(100) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Settlement]') AND [c].[name] = N'TOS');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Settlement] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Settlement] ALTER COLUMN [TOS] nvarchar(50) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Settlement]') AND [c].[name] = N'SettlementRemarks');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Settlement] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Settlement] ALTER COLUMN [SettlementRemarks] nvarchar(1000) NULL;
GO

ALTER TABLE [LoanAccountsProjection] ADD [LastCollectionAmount] decimal(18,2) NULL;
GO

ALTER TABLE [LoanAccountsProjection] ADD [LastCollectionMode] nvarchar(50) NULL;
GO

ALTER TABLE [GeoTagDetails] ADD [AccountId] nvarchar(32) NULL;
GO

ALTER TABLE [GeoTagDetails] ADD [TransactionSource] nvarchar(20) NULL;
GO

CREATE TABLE [CollectionProjection] (
    [Id] nvarchar(32) NOT NULL,
    [CollectionId] nvarchar(32) NOT NULL,
    [BUCKET] bigint NULL,
    [CURRENT_BUCKET] nvarchar(50) NULL,
    [NPA_STAGEID] nvarchar(50) NULL,
    [AgencyId] nvarchar(32) NULL,
    [CollectorId] nvarchar(32) NULL,
    [TeleCallingAgencyId] nvarchar(32) NULL,
    [TeleCallerId] nvarchar(32) NULL,
    [AllocationOwnerId] nvarchar(32) NULL,
    [BOM_POS] decimal(18,2) NULL,
    [CURRENT_POS] decimal(18,2) NULL,
    [PAYMENTSTATUS] nvarchar(50) NULL,
    [CURRENT_TOTAL_AMOUNT_DUE] decimal(16,2) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CollectionProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CollectionProjection_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_CollectionProjection_ApplicationOrg_TeleCallingAgencyId] FOREIGN KEY ([TeleCallingAgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_CollectionProjection_ApplicationUser_AllocationOwnerId] FOREIGN KEY ([AllocationOwnerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CollectionProjection_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CollectionProjection_ApplicationUser_TeleCallerId] FOREIGN KEY ([TeleCallerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CollectionProjection_Collections_CollectionId] FOREIGN KEY ([CollectionId]) REFERENCES [Collections] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FeedbackProjection] (
    [Id] nvarchar(32) NOT NULL,
    [FeedbackId] nvarchar(32) NOT NULL,
    [BUCKET] bigint NULL,
    [CURRENT_BUCKET] nvarchar(50) NULL,
    [NPA_STAGEID] nvarchar(50) NULL,
    [AgencyId] nvarchar(32) NULL,
    [CollectorId] nvarchar(32) NULL,
    [TeleCallingAgencyId] nvarchar(32) NULL,
    [TeleCallerId] nvarchar(32) NULL,
    [AllocationOwnerId] nvarchar(32) NULL,
    [BOM_POS] decimal(18,2) NULL,
    [CURRENT_POS] decimal(18,2) NULL,
    [PAYMENTSTATUS] nvarchar(50) NULL,
    [LastDispositionDate] datetime2 NULL,
    [LastDispositionCode] nvarchar(50) NULL,
    [LastDispositionCodeGroup] nvarchar(50) NULL,
    [LastPTPDate] datetime2 NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_FeedbackProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FeedbackProjection_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_FeedbackProjection_ApplicationOrg_TeleCallingAgencyId] FOREIGN KEY ([TeleCallingAgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_FeedbackProjection_ApplicationUser_AllocationOwnerId] FOREIGN KEY ([AllocationOwnerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_FeedbackProjection_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_FeedbackProjection_ApplicationUser_TeleCallerId] FOREIGN KEY ([TeleCallerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_FeedbackProjection_Feedback_FeedbackId] FOREIGN KEY ([FeedbackId]) REFERENCES [Feedback] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[AccountScopeConfiguration]'))
    SET IDENTITY_INSERT [AccountScopeConfiguration] ON;
INSERT INTO [AccountScopeConfiguration] ([Id], [AccountabilityTypeId], [CreatedBy], [CreatedDate], [IsDeleted], [LastModifiedBy], [LastModifiedDate], [Scope], [ScopeLevel])
VALUES (N'3a185d8db599c016d4caf7aa05af889f', N'AgencyToFrontEndExternalFOS', NULL, '2025-06-09T17:05:20.9485688+05:30', CAST(0 AS bit), NULL, '2025-06-09T17:05:20.9485720+05:30', N'all', 1),
(N'3a185d8db599d1ce3ace0b1c74528678', N'BankToFrontEndInternalFOS', NULL, '2025-06-09T17:05:20.9485740+05:30', CAST(0 AS bit), NULL, '2025-06-09T17:05:20.9485742+05:30', N'all', 1),
(N'3a185d8db599f4a83d63dec4faea8a98', N'AgencyToFrontEndExternalTC', NULL, '2025-06-09T17:05:20.9485733+05:30', CAST(0 AS bit), NULL, '2025-06-09T17:05:20.9485734+05:30', N'all', 1),
(N'3a185d8db599f686a3b157eaeb799b2d', N'BankToFrontEndInternalTC', NULL, '2025-06-09T17:05:20.9485759+05:30', CAST(0 AS bit), NULL, '2025-06-09T17:05:20.9485761+05:30', N'all', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[AccountScopeConfiguration]'))
    SET IDENTITY_INSERT [AccountScopeConfiguration] OFF;
GO

CREATE INDEX [IX_SettlementStatusHistory_ChangedByUserId] ON [SettlementStatusHistory] ([ChangedByUserId]);
GO

CREATE UNIQUE INDEX [IX_LoanAccountsProjection_LoanAccountId_Year_Month] ON [LoanAccountsProjection] ([LoanAccountId], [Year], [Month]) WHERE [Year] IS NOT NULL AND [Month] IS NOT NULL;
GO

CREATE INDEX [IX_GeoTagDetails_AccountId] ON [GeoTagDetails] ([AccountId]);
GO

CREATE INDEX [IX_CollectionProjection_AgencyId] ON [CollectionProjection] ([AgencyId]);
GO

CREATE INDEX [IX_CollectionProjection_AllocationOwnerId] ON [CollectionProjection] ([AllocationOwnerId]);
GO

CREATE INDEX [IX_CollectionProjection_CollectionId] ON [CollectionProjection] ([CollectionId]);
GO

CREATE INDEX [IX_CollectionProjection_CollectorId] ON [CollectionProjection] ([CollectorId]);
GO

CREATE INDEX [IX_CollectionProjection_TeleCallerId] ON [CollectionProjection] ([TeleCallerId]);
GO

CREATE INDEX [IX_CollectionProjection_TeleCallingAgencyId] ON [CollectionProjection] ([TeleCallingAgencyId]);
GO

CREATE INDEX [IX_FeedbackProjection_AgencyId] ON [FeedbackProjection] ([AgencyId]);
GO

CREATE INDEX [IX_FeedbackProjection_AllocationOwnerId] ON [FeedbackProjection] ([AllocationOwnerId]);
GO

CREATE INDEX [IX_FeedbackProjection_CollectorId] ON [FeedbackProjection] ([CollectorId]);
GO

CREATE INDEX [IX_FeedbackProjection_FeedbackId] ON [FeedbackProjection] ([FeedbackId]);
GO

CREATE INDEX [IX_FeedbackProjection_TeleCallerId] ON [FeedbackProjection] ([TeleCallerId]);
GO

CREATE INDEX [IX_FeedbackProjection_TeleCallingAgencyId] ON [FeedbackProjection] ([TeleCallingAgencyId]);
GO

ALTER TABLE [GeoTagDetails] ADD CONSTRAINT [FK_GeoTagDetails_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]);
GO

ALTER TABLE [SettlementStatusHistory] ADD CONSTRAINT [FK_SettlementStatusHistory_ApplicationUser_ChangedByUserId] FOREIGN KEY ([ChangedByUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250609113526_UpdatedGeoTagDetails', N'8.0.10');
GO

COMMIT;
GO

