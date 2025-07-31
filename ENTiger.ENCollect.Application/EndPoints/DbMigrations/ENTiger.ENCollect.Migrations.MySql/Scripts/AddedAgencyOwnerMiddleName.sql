START TRANSACTION;

ALTER TABLE `ApplicationOrg` ADD `PrimaryOwnerMiddleName` varchar(50) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250204110244_AddedAgencyOwnerMiddleName', '8.0.10');

COMMIT;

