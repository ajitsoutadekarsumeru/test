IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [FlexTenant] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NULL,
    [HostName] nvarchar(100) NULL,
    [TenantDbType] nvarchar(100) NULL,
    [DefaultWriteDbConnectionString] nvarchar(500) NOT NULL,
    [DefaultReadDbConnectionString] nvarchar(500) NOT NULL,
    [IsSharedTenant] bit NOT NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [IsDeleted] bit NULL,
    [CreatedDate] datetimeoffset NULL,
    [LastModifiedDate] datetimeoffset NULL,
    [Color] nvarchar(50) NULL,
    [Logo] nvarchar(50) NULL,
    CONSTRAINT [PK_FlexTenant] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TenantEmailConfiguration] (
    [Id] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [TenantId] nvarchar(32) NULL,
    [Mailcc] nvarchar(500) NULL,
    [MailFrom] nvarchar(100) NULL,
    [EmailLogPath] nvarchar(200) NULL,
    [MailTo] nvarchar(100) NULL,
    [MailSignature] nvarchar(100) NULL,
    [SmtpServer] nvarchar(100) NULL,
    [SmtpPort] nvarchar(20) NULL,
    [SmtpUser] nvarchar(100) NULL,
    [SmtpPassword] nvarchar(100) NULL,
    [EnableSsl] nvarchar(50) NULL,
    CONSTRAINT [PK_TenantEmailConfiguration] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantEmailConfiguration_FlexTenant_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [FlexTenant] ([Id])
);
GO

CREATE TABLE [TenantSMSConfiguration] (
    [Id] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [TenantId] nvarchar(32) NULL,
    [Key] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_TenantSMSConfiguration] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantSMSConfiguration_FlexTenant_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [FlexTenant] ([Id])
);
GO

CREATE INDEX [IX_TenantEmailConfiguration_TenantId] ON [TenantEmailConfiguration] ([TenantId]);
GO

CREATE INDEX [IX_TenantSMSConfiguration_TenantId] ON [TenantSMSConfiguration] ([TenantId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240802105525_initial', N'8.0.6');
GO

COMMIT;
GO

