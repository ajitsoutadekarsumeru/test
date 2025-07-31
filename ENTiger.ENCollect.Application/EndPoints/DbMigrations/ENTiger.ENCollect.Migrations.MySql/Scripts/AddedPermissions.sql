START TRANSACTION;

CREATE TABLE `Permissions` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PermissionName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(500) CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Permissions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DesignationPermissions` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PermissionId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DesignationPermissions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DesignationPermissions_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_DesignationPermissions_Permissions_PermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `Permissions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_DesignationPermissions_DesignationId` ON `DesignationPermissions` (`DesignationId`);

CREATE INDEX `IX_DesignationPermissions_PermissionId` ON `DesignationPermissions` (`PermissionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250131130954_AddedPermissions', '8.0.10');

COMMIT;

