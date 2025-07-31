START TRANSACTION;

DROP TABLE `BucketHeatMapConfig`;

ALTER TABLE `Resolutions` DROP COLUMN `Name`;

ALTER TABLE `Resolutions` ADD `Code` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE TABLE `HeatMapConfig` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HeatMapType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RowId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ColumnId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `RangeFrom` int NOT NULL,
    `RangeTo` int NOT NULL,
    `HeatIndicator` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_HeatMapConfig` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;




INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250620062207_ModifiedHeatMapConfigFieldChanges', '8.0.10');

COMMIT;

