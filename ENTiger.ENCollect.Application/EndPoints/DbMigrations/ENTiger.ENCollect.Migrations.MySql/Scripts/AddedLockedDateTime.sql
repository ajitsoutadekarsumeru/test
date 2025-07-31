START TRANSACTION;

ALTER TABLE `ApplicationUser` ADD `LockedDateTime` datetime(6) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250224112036_AddedLockedDateTime', '8.0.10');

COMMIT;

