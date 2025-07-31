BEGIN TRANSACTION;
GO

CREATE TABLE [AuditTrailRecord] (
    [Id] nvarchar(32) NOT NULL,
    [EntityId] nvarchar(32) NOT NULL,
    [EntityType] nvarchar(100) NOT NULL,
    [Operation] nvarchar(100) NOT NULL,
    [DiffJson] nvarchar(max) NOT NULL,
    [ClientIP] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AuditTrailRecord] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250131042822_AddedAuditTrailTable', N'8.0.10');
GO

COMMIT;
GO

