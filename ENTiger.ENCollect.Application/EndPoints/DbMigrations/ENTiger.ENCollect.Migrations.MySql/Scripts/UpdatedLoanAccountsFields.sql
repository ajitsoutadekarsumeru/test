START TRANSACTION;

ALTER TABLE `LoanAccounts` DROP COLUMN `UnAttempted`;

ALTER TABLE `LoanAccounts` MODIFY COLUMN `Paid` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `LoanAccounts` MODIFY COLUMN `Attempted` tinyint(1) NOT NULL DEFAULT FALSE;


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250611041838_UpdatedLoanAccountsFields', '8.0.10');

COMMIT;

