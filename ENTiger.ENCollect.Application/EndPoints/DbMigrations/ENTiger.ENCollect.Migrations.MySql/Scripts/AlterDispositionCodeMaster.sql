START TRANSACTION;

ALTER TABLE `DispositionCodeMaster` DROP COLUMN `DispositionCodeCustomerOrAccountLevel`;

ALTER TABLE `DispositionCodeMaster` ADD `DispositionCodeIsCustomerLevel` tinyint(1) NOT NULL DEFAULT FALSE;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-04-11 06:50:46', `LastModifiedDate` = TIMESTAMP '2025-04-11 06:50:46'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-04-11 06:50:46', `LastModifiedDate` = TIMESTAMP '2025-04-11 06:50:46'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-04-11 06:50:46', `LastModifiedDate` = TIMESTAMP '2025-04-11 06:50:46'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-04-11 06:50:46', `LastModifiedDate` = TIMESTAMP '2025-04-11 06:50:46'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250411065048_AlterDispositionCodeMaster', '8.0.10');

COMMIT;

