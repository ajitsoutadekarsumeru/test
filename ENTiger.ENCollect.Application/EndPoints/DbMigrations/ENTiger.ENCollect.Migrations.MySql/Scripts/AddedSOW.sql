START TRANSACTION;

ALTER TABLE `ApplicationUser` DROP COLUMN `MaxHotleadCount`;

ALTER TABLE `ApplicationUser` ADD `GeoLevelId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `ProductLevelId` varchar(32) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `UserBucketScope` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BucketScopeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserBucketScope` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserBucketScope_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_UserBucketScope_Buckets_BucketScopeId` FOREIGN KEY (`BucketScopeId`) REFERENCES `Buckets` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserGeoScope` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `GeoScopeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserGeoScope` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserGeoScope_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_UserGeoScope_HierarchyMaster_GeoScopeId` FOREIGN KEY (`GeoScopeId`) REFERENCES `HierarchyMaster` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserProductScope` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ProductScopeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserProductScope` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserProductScope_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_UserProductScope_HierarchyMaster_ProductScopeId` FOREIGN KEY (`ProductScopeId`) REFERENCES `HierarchyMaster` (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-18 09:42:41', `LastModifiedDate` = TIMESTAMP '2025-06-18 09:42:41'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-18 09:42:41', `LastModifiedDate` = TIMESTAMP '2025-06-18 09:42:41'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-18 09:42:41', `LastModifiedDate` = TIMESTAMP '2025-06-18 09:42:41'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-18 09:42:41', `LastModifiedDate` = TIMESTAMP '2025-06-18 09:42:41'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_ApplicationUser_GeoLevelId` ON `ApplicationUser` (`GeoLevelId`);

CREATE INDEX `IX_ApplicationUser_ProductLevelId` ON `ApplicationUser` (`ProductLevelId`);

CREATE INDEX `IX_UserBucketScope_ApplicationUserId` ON `UserBucketScope` (`ApplicationUserId`);

CREATE INDEX `IX_UserBucketScope_BucketScopeId` ON `UserBucketScope` (`BucketScopeId`);

CREATE INDEX `IX_UserGeoScope_ApplicationUserId` ON `UserGeoScope` (`ApplicationUserId`);

CREATE INDEX `IX_UserGeoScope_GeoScopeId` ON `UserGeoScope` (`GeoScopeId`);

CREATE INDEX `IX_UserProductScope_ApplicationUserId` ON `UserProductScope` (`ApplicationUserId`);

CREATE INDEX `IX_UserProductScope_ProductScopeId` ON `UserProductScope` (`ProductScopeId`);

ALTER TABLE `ApplicationUser` ADD CONSTRAINT `FK_ApplicationUser_HierarchyLevel_GeoLevelId` FOREIGN KEY (`GeoLevelId`) REFERENCES `HierarchyLevel` (`Id`);

ALTER TABLE `ApplicationUser` ADD CONSTRAINT `FK_ApplicationUser_HierarchyLevel_ProductLevelId` FOREIGN KEY (`ProductLevelId`) REFERENCES `HierarchyLevel` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250618094246_AddedSOW', '8.0.10');

COMMIT;

