START TRANSACTION;

ALTER TABLE `LoanAccounts` ADD `Latest_Address_From_Trail` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `Latest_Email_From_Trail` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `Latest_Number_From_Receipt` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `Latest_Number_From_Send_Payment` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `Latest_Number_From_Trail` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `SegmentId` varchar(32) CHARACTER SET utf8mb4 NULL;

CREATE INDEX `IX_LoanAccounts_SegmentId` ON `LoanAccounts` (`SegmentId`);

ALTER TABLE `LoanAccounts` ADD CONSTRAINT `FK_LoanAccounts_Segmentation_SegmentId` FOREIGN KEY (`SegmentId`) REFERENCES `Segmentation` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241015120613_AddedAccountContactDetails', '8.0.6');

COMMIT;

