START TRANSACTION;

ALTER TABLE `Feedback` ADD `ModeOfCommunication` varchar(100) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250103100124_AddedModeOfCommunicationInFeedback', '8.0.6');

COMMIT;

