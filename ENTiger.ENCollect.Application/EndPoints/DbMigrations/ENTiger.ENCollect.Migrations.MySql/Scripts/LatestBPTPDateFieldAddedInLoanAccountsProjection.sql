START TRANSACTION;

ALTER TABLE `LoanAccountsProjection` ADD `LatestBPTPDate` datetime(6) NULL;




INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250610095525_LatestBPTPDateFieldAddedInLoanAccountsProjection', '8.0.10');

COMMIT;

