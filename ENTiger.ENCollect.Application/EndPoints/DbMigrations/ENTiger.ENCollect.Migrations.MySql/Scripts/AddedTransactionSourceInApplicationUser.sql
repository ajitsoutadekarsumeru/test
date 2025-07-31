START TRANSACTION;

ALTER TABLE `ApplicationUser` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationOrg` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250318084541_AddedTransactionSourceInApplicationUser', '8.0.10');

COMMIT;

