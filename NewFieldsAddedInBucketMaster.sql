START TRANSACTION;

ALTER TABLE `Resolutions` MODIFY COLUMN `Name` int NOT NULL DEFAULT 0;

ALTER TABLE `Buckets` ADD `DPD_From` int NOT NULL DEFAULT 0;

ALTER TABLE `Buckets` ADD `DPD_To` int NOT NULL DEFAULT 0;

ALTER TABLE `Buckets` ADD `DisplayLabel` varchar(50) CHARACTER SET utf8mb4 NULL;


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250617133537_NewFieldsAddedInBucketMaster', '8.0.10');

COMMIT;

