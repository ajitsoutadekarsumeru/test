START TRANSACTION;

ALTER TABLE `PermissionSchemeChangeLog` MODIFY COLUMN `RemovedPermissions` varchar(2000) CHARACTER SET utf8mb4 NULL;

UPDATE `PermissionSchemeChangeLog` SET `Remarks` = ''
WHERE `Remarks` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `PermissionSchemeChangeLog` MODIFY COLUMN `Remarks` varchar(500) CHARACTER SET utf8mb4 NOT NULL;

UPDATE `PermissionSchemeChangeLog` SET `PermissionSchemeId` = ''
WHERE `PermissionSchemeId` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `PermissionSchemeChangeLog` MODIFY COLUMN `PermissionSchemeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL;

UPDATE `PermissionSchemeChangeLog` SET `ChangeType` = ''
WHERE `ChangeType` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `PermissionSchemeChangeLog` MODIFY COLUMN `ChangeType` varchar(50) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `PermissionSchemeChangeLog` MODIFY COLUMN `AddedPermissions` varchar(2000) CHARACTER SET utf8mb4 NULL;

UPDATE `Permissions` SET `Name` = ''
WHERE `Name` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `Permissions` MODIFY COLUMN `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-14 04:09:14', `LastModifiedDate` = TIMESTAMP '2025-05-14 04:09:14'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-14 04:09:14', `LastModifiedDate` = TIMESTAMP '2025-05-14 04:09:14'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-14 04:09:14', `LastModifiedDate` = TIMESTAMP '2025-05-14 04:09:14'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-14 04:09:14', `LastModifiedDate` = TIMESTAMP '2025-05-14 04:09:14'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250514040917_UpdatedPermsissionScheme', '8.0.10');

COMMIT;

