START TRANSACTION;

CREATE TABLE `Communication` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ActorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Channel` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `MessageBody` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MessageSubject` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Recipient` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `RecipientType` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Language` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `ErrorMessage` longtext CHARACTER SET utf8mb4 NULL,
    `MetadataJson` longtext CHARACTER SET utf8mb4 NULL,
    `DispatchedAt` datetime(6) NULL,
    `DeliveredAt` datetime(6) NULL,
    `ReadAt` datetime(6) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Communication` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250722112515_AddedCommunicationEntity', '8.0.10');

COMMIT;

