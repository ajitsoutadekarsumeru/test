START TRANSACTION;

ALTER TABLE `UserAttendanceLog` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `PayInSlips` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Feedback` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` ADD `DispositionCodeCustomerOrAccountLevel` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Collections` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `applicationuser` ADD `TransactionSource` varchar(20) CHARACTER SET utf8mb4 NULL;


UPDATE `AgencyType` SET `SubType` = 'field agents'
WHERE `Id` = 'ff379ce22f7b4aca9e74d0dadccb3739';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250224110123_TransactionSourceFieldAdded', '8.0.10');

COMMIT;



update dispositiongroupmaster a inner join dispositioncodemaster b on
b.DispositionGroupMasterId = a.Id 
set b.dispositioncodecustomeroraccountlevel = 'Customer Level'
where a.Name = 'Contact' and b.DispositionCode in ('Deceased','CB','LM');


update dispositiongroupmaster a inner join dispositioncodemaster b on
b.DispositionGroupMasterId = a.Id 
set b.dispositioncodecustomeroraccountlevel = 'Customer Level'
where a.Name = 'No Contact' and b.DispositionCode in ('CNF','ANF','BUSYLINE','DL','WN','OOS');


