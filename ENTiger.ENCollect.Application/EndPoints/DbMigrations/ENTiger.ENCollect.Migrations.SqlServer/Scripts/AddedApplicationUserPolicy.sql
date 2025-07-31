BEGIN TRANSACTION;
GO

ALTER TABLE [ApplicationUser] ADD [IsPolicyAccepted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ApplicationUser] ADD [PolicyAcceptedDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209023850_AddedApplicationUserPolicy', N'8.0.10');
GO

COMMIT;
GO

