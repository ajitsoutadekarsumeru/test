BEGIN TRANSACTION;
GO

CREATE TABLE [CustomerConsent] (
    [Id] nvarchar(32) NOT NULL,
    [AccountId] nvarchar(32) NULL,
    [UserId] nvarchar(32) NULL,
    [RequestedVisitTime] datetime2 NULL,
    [ConsentResponseTime] datetime2 NULL,
    [ExpiryTime] datetime2 NULL,
    [IsActive] bit NULL,
    [Status] nvarchar(max) NOT NULL,
    [SecureToken] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CustomerConsent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerConsent_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CustomerConsent_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id])
);
GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:33:22.5319426+02:00', [LastModifiedDate] = '2025-03-25T09:33:22.5319461+02:00'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:33:22.5319472+02:00', [LastModifiedDate] = '2025-03-25T09:33:22.5319472+02:00'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:33:22.5319466+02:00', [LastModifiedDate] = '2025-03-25T09:33:22.5319467+02:00'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [RoleAccountScope] SET [CreatedDate] = '2025-03-25T09:33:22.5319480+02:00', [LastModifiedDate] = '2025-03-25T09:33:22.5319481+02:00'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_CustomerConsent_AccountId] ON [CustomerConsent] ([AccountId]);
GO

CREATE INDEX [IX_CustomerConsent_UserId] ON [CustomerConsent] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250325073325_AddedCustomerConsent', N'8.0.10');
GO

COMMIT;
GO

