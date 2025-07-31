START TRANSACTION;

ALTER TABLE `LoanAccountsProjection` ADD `LastCollectionAmount` decimal(65,30) NULL;

ALTER TABLE `LoanAccountsProjection` ADD `LastCollectionMode` varchar(50) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250526140349_NewFieldsAddedInLoanAccountsProjection', '8.0.10');

COMMIT;

