BEGIN TRANSACTION;
GO

CREATE TABLE [HierarchyLevel] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Order] int NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_HierarchyLevel] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [HierarchyMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Item] nvarchar(50) NOT NULL,
    [LevelId] nvarchar(32) NOT NULL,
    [ParentId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_HierarchyMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HierarchyMaster_HierarchyLevel_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [HierarchyLevel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HierarchyMaster_HierarchyMaster_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [HierarchyMaster] ([Id])
);
GO

CREATE INDEX [IX_HierarchyMaster_LevelId] ON [HierarchyMaster] ([LevelId]);
GO

CREATE INDEX [IX_HierarchyMaster_ParentId] ON [HierarchyMaster] ([ParentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250612094353_AddedHierarchy', N'8.0.10');
GO

COMMIT;
GO

