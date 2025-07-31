BEGIN TRANSACTION;
GO

ALTER TABLE [CommunicationTrigger] DROP CONSTRAINT [FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId];
GO

DROP TABLE [CommunicationTriggerType];
GO

EXEC sp_rename N'[CommunicationTrigger].[CommunicationTriggerTypeId]', N'TriggerTypeId', N'COLUMN';
GO

EXEC sp_rename N'[CommunicationTrigger].[IX_CommunicationTrigger_CommunicationTriggerTypeId]', N'IX_CommunicationTrigger_TriggerTypeId', N'INDEX';
GO

CREATE TABLE [TriggerType] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TriggerType] PRIMARY KEY ([Id])
);
GO



GO

ALTER TABLE [CommunicationTrigger] ADD CONSTRAINT [FK_CommunicationTrigger_TriggerType_TriggerTypeId] FOREIGN KEY ([TriggerTypeId]) REFERENCES [TriggerType] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620050122_AddedCommunicationTriggers', N'8.0.10');
GO

COMMIT;
GO

