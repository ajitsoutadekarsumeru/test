START TRANSACTION;

ALTER TABLE `SettlementStatusHistory` ADD `Action` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Settlement` ADD `CancellationReason` varchar(1000) CHARACTER SET utf8mb4 NULL;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-07-14 09:50:29', `LastModifiedDate` = TIMESTAMP '2025-07-14 09:50:29'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-07-14 09:50:29', `LastModifiedDate` = TIMESTAMP '2025-07-14 09:50:29'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-07-14 09:50:29', `LastModifiedDate` = TIMESTAMP '2025-07-14 09:50:29'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-07-14 09:50:29', `LastModifiedDate` = TIMESTAMP '2025-07-14 09:50:29'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250715073815_Settlement_Cancellation', '8.0.10');

COMMIT;

