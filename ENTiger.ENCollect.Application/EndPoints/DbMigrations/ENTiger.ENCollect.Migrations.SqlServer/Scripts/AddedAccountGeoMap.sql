BEGIN TRANSACTION;
GO

CREATE TABLE [AccountGeoMap] (
    [Id] nvarchar(32) NOT NULL,
    [AccountId] nvarchar(32) NOT NULL,
    [HierarchyId] nvarchar(32) NOT NULL,
    [HierarchyLevel] int NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountGeoMap] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountGeoMap_HierarchyMaster_HierarchyId] FOREIGN KEY ([HierarchyId]) REFERENCES [HierarchyMaster] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountGeoMap_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-20T16:05:20.1216624+05:30', [LastModifiedDate] = '2025-06-20T16:05:20.1216698+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-20T16:05:20.1216795+05:30', [LastModifiedDate] = '2025-06-20T16:05:20.1216800+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-20T16:05:20.1216759+05:30', [LastModifiedDate] = '2025-06-20T16:05:20.1216766+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-06-20T16:05:20.1216825+05:30', [LastModifiedDate] = '2025-06-20T16:05:20.1216831+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_AccountGeoMap_AccountId] ON [AccountGeoMap] ([AccountId]);
GO

CREATE INDEX [IX_AccountGeoMap_HierarchyId] ON [AccountGeoMap] ([HierarchyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620103525_AddedAccountGeoMap', N'8.0.10');
GO

COMMIT;
GO

