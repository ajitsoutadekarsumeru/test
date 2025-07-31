BEGIN TRANSACTION;
GO

CREATE TABLE [LoanAccountFlag] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [IsActive] bit NOT NULL,
    [LoanAccountId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoanAccountFlag] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccountFlag_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LoanAccountNote] (
    [Id] nvarchar(32) NOT NULL,
    [Code] nvarchar(50) NULL,
    [Description] nvarchar(500) NULL,
    [LoanAccountId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoanAccountNote] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccountNote_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_LoanAccountFlag_LoanAccountId] ON [LoanAccountFlag] ([LoanAccountId]);
GO

CREATE INDEX [IX_LoanAccountNote_LoanAccountId] ON [LoanAccountNote] ([LoanAccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241016042339_AddedAccountNotesAndFlags', N'8.0.6');
GO

COMMIT;
GO

