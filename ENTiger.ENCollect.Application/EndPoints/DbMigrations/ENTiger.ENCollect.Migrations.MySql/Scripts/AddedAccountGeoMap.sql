START TRANSACTION;

CREATE TABLE `AccountGeoMap` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyLevel` int NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountGeoMap` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccountGeoMap_HierarchyMaster_HierarchyId` FOREIGN KEY (`HierarchyId`) REFERENCES `HierarchyMaster` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AccountGeoMap_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-20 10:29:23', `LastModifiedDate` = TIMESTAMP '2025-06-20 10:29:23'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-20 10:29:23', `LastModifiedDate` = TIMESTAMP '2025-06-20 10:29:23'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-20 10:29:23', `LastModifiedDate` = TIMESTAMP '2025-06-20 10:29:23'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-20 10:29:23', `LastModifiedDate` = TIMESTAMP '2025-06-20 10:29:23'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_AccountGeoMap_AccountId` ON `AccountGeoMap` (`AccountId`);

CREATE INDEX `IX_AccountGeoMap_HierarchyId` ON `AccountGeoMap` (`HierarchyId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250620102928_AddedAccountGeoMap', '8.0.10');

COMMIT;

