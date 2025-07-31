START TRANSACTION;

ALTER TABLE `ApplicationUser` ADD `UsedWallet` decimal(18,2) NOT NULL DEFAULT 0.0;

ALTER TABLE `ApplicationUser` ADD `WalletLastUpdatedDate` datetime(6) NULL;

ALTER TABLE `ApplicationUser` ADD `WalletLimit` decimal(18,2) NOT NULL DEFAULT 0.0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250225140503_AddedUserCashWalletLimit', '8.0.10');

COMMIT;

