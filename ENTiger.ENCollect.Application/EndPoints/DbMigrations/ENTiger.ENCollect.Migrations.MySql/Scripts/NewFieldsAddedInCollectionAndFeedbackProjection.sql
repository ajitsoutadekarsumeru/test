START TRANSACTION;

ALTER TABLE `FeedbackProjection` ADD `LastDispositionCode` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `FeedbackProjection` ADD `LastDispositionCodeGroup` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `FeedbackProjection` ADD `LastDispositionDate` datetime(6) NULL;

ALTER TABLE `FeedbackProjection` ADD `LastPTPDate` datetime(6) NULL;

ALTER TABLE `CollectionProjection` ADD `CURRENT_TOTAL_AMOUNT_DUE` decimal(16,2) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250602112641_NewFieldsAddedInCollectionAndFeedbackProjection', '8.0.10');

COMMIT;

