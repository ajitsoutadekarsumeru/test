BEGIN TRANSACTION;
GO

DROP TABLE [TriggerTemplateMapping];
GO

ALTER TABLE [TriggerType] ADD [CustomName] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [TriggerType] ADD [EntryPoint] nvarchar(150) NOT NULL DEFAULT N'';
GO

ALTER TABLE [TriggerType] ADD [OffsetBasis] nvarchar(50) NULL;
GO

ALTER TABLE [TriggerType] ADD [OffsetDirection] nvarchar(50) NULL;
GO

ALTER TABLE [TriggerType] ADD [RequiresDaysOffset] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SettlementStatusHistory] ADD [Action] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [CancellationReason] nvarchar(1000) NULL;
GO

ALTER TABLE [CommunicationTrigger] ADD [RecipientType] nvarchar(150) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplate] ADD [EntryPoint] nvarchar(150) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplate] ADD [RecipientType] nvarchar(150) NOT NULL DEFAULT N'';
GO

CREATE TABLE [TriggerAccountQueueProjection] (
    [Id] nvarchar(32) NOT NULL,
    [RunId] nvarchar(max) NOT NULL,
    [TriggerId] nvarchar(32) NOT NULL,
    [AccountId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TriggerAccountQueueProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TriggerAccountQueueProjection_CommunicationTrigger_TriggerId] FOREIGN KEY ([TriggerId]) REFERENCES [CommunicationTrigger] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TriggerAccountQueueProjection_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TriggerDeliverySpec] (
    [Id] nvarchar(32) NOT NULL,
    [CommunicationTriggerId] nvarchar(32) NOT NULL,
    [CommunicationTemplateId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TriggerDeliverySpec] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TriggerDeliverySpec_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TriggerDeliverySpec_CommunicationTrigger_CommunicationTriggerId] FOREIGN KEY ([CommunicationTriggerId]) REFERENCES [CommunicationTrigger] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-14T15:20:29.2450000+05:30', [LastModifiedDate] = '2025-07-14T15:20:29.2450000+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-14T15:20:29.2450000+05:30', [LastModifiedDate] = '2025-07-14T15:20:29.2450000+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-14T15:20:29.2450000+05:30', [LastModifiedDate] = '2025-07-14T15:20:29.2450000+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-07-14T15:20:29.2450000+05:30', [LastModifiedDate] = '2025-07-14T15:20:29.2450000+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_TriggerAccountQueueProjection_AccountId] ON [TriggerAccountQueueProjection] ([AccountId]);
GO

CREATE INDEX [IX_TriggerAccountQueueProjection_TriggerId] ON [TriggerAccountQueueProjection] ([TriggerId]);
GO

CREATE INDEX [IX_TriggerDeliverySpec_CommunicationTemplateId] ON [TriggerDeliverySpec] ([CommunicationTemplateId]);
GO

CREATE INDEX [IX_TriggerDeliverySpec_CommunicationTriggerId] ON [TriggerDeliverySpec] ([CommunicationTriggerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250716105036_AddRecipientTypeIntoCommunicationTrigger', N'8.0.10');
GO

COMMIT;
GO

