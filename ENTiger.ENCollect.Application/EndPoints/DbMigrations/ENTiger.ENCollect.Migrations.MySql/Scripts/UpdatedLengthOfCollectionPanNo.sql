START TRANSACTION;

ALTER TABLE `Collections` MODIFY COLUMN `yPANNo` varchar(200) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250715055214_UpdatedLengthOfCollectionPanNo', '8.0.10');

COMMIT;

