START TRANSACTION;

ALTER TABLE `LoanAccounts` ADD `LatestAllocationDate` datetime(6) NULL;



INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250403111336_LatestAllocationDateFieldAdded', '8.0.10');

COMMIT;

