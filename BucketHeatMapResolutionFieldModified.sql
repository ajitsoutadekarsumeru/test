START TRANSACTION;

ALTER TABLE `BucketHeatMapConfig` DROP FOREIGN KEY `FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId`;

ALTER TABLE `BucketHeatMapConfig` DROP INDEX `IX_BucketHeatMapConfig_ResolutionMasterId`;

ALTER TABLE `BucketHeatMapConfig` DROP COLUMN `ResolutionMasterId`;

ALTER TABLE `BucketHeatMapConfig` RENAME COLUMN `Resolutions` TO `ResolutionId`;



CREATE INDEX `IX_BucketHeatMapConfig_ResolutionId` ON `BucketHeatMapConfig` (`ResolutionId`);

ALTER TABLE `BucketHeatMapConfig` ADD CONSTRAINT `FK_BucketHeatMapConfig_Resolutions_ResolutionId` FOREIGN KEY (`ResolutionId`) REFERENCES `Resolutions` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250618131500_BucketHeatMapResolutionFieldModified', '8.0.10');

COMMIT;

