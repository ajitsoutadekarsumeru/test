START TRANSACTION;

ALTER TABLE `Settlement` MODIFY COLUMN `TOS` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` MODIFY COLUMN `SettlementRemarks` varchar(1000) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `GeoTagDetails` ADD `AccountId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `GeoTagDetails` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-09 11:27:24', `LastModifiedDate` = TIMESTAMP '2025-06-09 11:27:24'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-09 11:27:24', `LastModifiedDate` = TIMESTAMP '2025-06-09 11:27:24'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-09 11:27:24', `LastModifiedDate` = TIMESTAMP '2025-06-09 11:27:24'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-09 11:27:24', `LastModifiedDate` = TIMESTAMP '2025-06-09 11:27:24'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_GeoTagDetails_AccountId` ON `GeoTagDetails` (`AccountId`);

ALTER TABLE `GeoTagDetails` ADD CONSTRAINT `FK_GeoTagDetails_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250609112727_UpdatedGeoTagDetails', '8.0.10');

COMMIT;

