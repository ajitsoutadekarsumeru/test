START TRANSACTION;

ALTER TABLE `LoanAccounts` DROP COLUMN `LastXDigitsOfAgreementId`;

ALTER TABLE `LoanAccounts` DROP COLUMN `LastXDigitsOfPrimaryCard`;

ALTER TABLE `LoanAccounts` ADD `ReverseOfAgreementId` varchar(255) CHARACTER SET utf8mb4 AS (REVERSE(AgreementId)) STORED NULL;

ALTER TABLE `LoanAccounts` ADD `ReverseOfPrimaryCard` varchar(255) CHARACTER SET utf8mb4 AS (REVERSE(PRIMARY_CARD_NUMBER)) STORED NULL;

UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:20:48', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:20:48'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:20:48', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:20:48'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:20:48', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:20:48'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:20:48', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:20:48'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_LoanAccounts_ReverseOfAgreementId` ON `LoanAccounts` (`ReverseOfAgreementId`);

CREATE INDEX `IX_LoanAccounts_ReverseOfPrimaryCard` ON `LoanAccounts` (`ReverseOfPrimaryCard`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250325072051_AddedLastXDigitsOfAccountSearch', '8.0.10');

COMMIT;

