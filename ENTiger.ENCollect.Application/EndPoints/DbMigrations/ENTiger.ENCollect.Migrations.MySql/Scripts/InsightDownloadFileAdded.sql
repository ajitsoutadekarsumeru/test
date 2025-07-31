START TRANSACTION;

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

UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-07 06:49:10', `LastModifiedDate` = TIMESTAMP '2025-03-07 06:49:10'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `AccountabilityTypeId` = 'BankToFrontEndInternalFOS', `CreatedDate` = TIMESTAMP '2025-03-07 06:49:10', `LastModifiedDate` = TIMESTAMP '2025-03-07 06:49:10'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-07 06:49:10', `LastModifiedDate` = TIMESTAMP '2025-03-07 06:49:10'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `AccountabilityTypeId` = 'BankToFrontEndInternalTC', `CreatedDate` = TIMESTAMP '2025-03-07 06:49:10', `LastModifiedDate` = TIMESTAMP '2025-03-07 06:49:10'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250307064913_InsightDownloadFileAdded', '8.0.10');

COMMIT;

