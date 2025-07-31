BEGIN TRANSACTION;
GO

DROP TABLE [DesignationPermissions_SeedData];
GO

DROP TABLE [Permissions_SeedData];
GO

ALTER TABLE [ApplicationOrg] ADD [PrimaryOwnerMiddleName] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250204070315_AddedAgencyOwnerMiddleName', N'8.0.10');
GO

COMMIT;
GO

