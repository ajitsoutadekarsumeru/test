START TRANSACTION;

ALTER TABLE `ApplicationUser` ADD `BloodGroup` varchar(5) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `EmergencyContactNo` varchar(10) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240926094719_ApplicationUser_newFields', '8.0.6');

COMMIT;

