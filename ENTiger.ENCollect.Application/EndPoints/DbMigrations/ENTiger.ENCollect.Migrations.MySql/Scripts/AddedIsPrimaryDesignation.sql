START TRANSACTION;

ALTER TABLE `CompanyUserDesignation` ADD `IsPrimaryDesignation` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `AgencyUserDesignation` ADD `IsPrimaryDesignation` tinyint(1) NOT NULL DEFAULT FALSE;

CREATE TABLE `InsightDownloadFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_InsightDownloadFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Settlement` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Settlement` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-06 08:20:07', `LastModifiedDate` = TIMESTAMP '2025-05-06 08:20:07'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-06 08:20:07', `LastModifiedDate` = TIMESTAMP '2025-05-06 08:20:07'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-06 08:20:07', `LastModifiedDate` = TIMESTAMP '2025-05-06 08:20:07'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-05-06 08:20:07', `LastModifiedDate` = TIMESTAMP '2025-05-06 08:20:07'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250506082010_AddedIsPrimaryDesignation', '8.0.10');

COMMIT;

