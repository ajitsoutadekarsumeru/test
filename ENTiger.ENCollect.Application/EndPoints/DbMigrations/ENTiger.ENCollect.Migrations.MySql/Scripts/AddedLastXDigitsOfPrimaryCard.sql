START TRANSACTION;

ALTER TABLE `LoanAccounts` ADD `LastXDigitsOfPrimaryCard` varchar(25) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250207074259_AddedLastXDigitsOfPrimaryCard', '8.0.10');

COMMIT;

