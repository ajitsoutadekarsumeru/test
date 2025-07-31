START TRANSACTION;

ALTER TABLE `Permissions` DROP COLUMN `IsActive`;

ALTER TABLE `Permissions` RENAME COLUMN `PermissionName` TO `Name`;

ALTER TABLE `Permissions` MODIFY COLUMN `Description` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Permissions` ADD `Section` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Designation` ADD `PermissionSchemeId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `UserType` varchar(20) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE TABLE `PermissionSchemeChangeLog` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PermissionSchemeId` longtext CHARACTER SET utf8mb4 NULL,
    `AddedPermissions` longtext CHARACTER SET utf8mb4 NULL,
    `RemovedPermissions` longtext CHARACTER SET utf8mb4 NULL,
    `ChangeType` longtext CHARACTER SET utf8mb4 NULL,
    `Remarks` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PermissionSchemeChangeLog` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PermissionSchemes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Remarks` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PermissionSchemes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EnabledPermission` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PermissionId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PermissionSchemeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_EnabledPermission` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_EnabledPermission_PermissionSchemes_PermissionSchemeId` FOREIGN KEY (`PermissionSchemeId`) REFERENCES `PermissionSchemes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EnabledPermission_Permissions_PermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `Permissions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-07 17:02:25', `LastModifiedDate` = TIMESTAMP '2025-05-07 17:02:25'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-07 17:02:25', `LastModifiedDate` = TIMESTAMP '2025-05-07 17:02:25'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-07 17:02:25', `LastModifiedDate` = TIMESTAMP '2025-05-07 17:02:25'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-07 17:02:25', `LastModifiedDate` = TIMESTAMP '2025-05-07 17:02:25'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_Designation_PermissionSchemeId` ON `Designation` (`PermissionSchemeId`);

CREATE INDEX `IX_EnabledPermission_PermissionId` ON `EnabledPermission` (`PermissionId`);

CREATE INDEX `IX_EnabledPermission_PermissionSchemeId` ON `EnabledPermission` (`PermissionSchemeId`);

ALTER TABLE `Designation` ADD CONSTRAINT `FK_Designation_PermissionSchemes_PermissionSchemeId` FOREIGN KEY (`PermissionSchemeId`) REFERENCES `PermissionSchemes` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250507170228_AddedPermissionScheme', '8.0.10');

COMMIT;

