START TRANSACTION;

ALTER TABLE `LoanAccounts` ADD `LastXDigitsOfAgreementId` varchar(50) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250207063702_AddedLastXDigitsOfAccountNumber', '8.0.10');

COMMIT;

