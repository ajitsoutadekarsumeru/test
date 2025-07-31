START TRANSACTION;

ALTER TABLE `ApplicationUser` DROP COLUMN `UsedWallet`;

ALTER TABLE `ApplicationUser` DROP COLUMN `WalletLastUpdatedDate`;

ALTER TABLE `ApplicationUser` DROP COLUMN `WalletLimit`;

UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:26:36', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:26:36'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:26:36', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:26:36'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:26:36', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:26:36'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:26:36', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:26:36'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250325082638_RemovedDirectWalletFieldsFromAppUser', '8.0.10');

COMMIT;

