update Designation set Level = 1 where Id in  ('BranchCashier','BranchManager');
update Designation set Level = 2 where Id in  ('BranchSupervisor','CollectionsManager','CollectionsSupervisor');
update Designation set Level = 3 where Id in  ('BucketCollectionsHead','CityCollectionsHead');
update Designation set Level = 4 where Id in  ('ProductCollectionsHead'); 
update Designation set Level = 5 where Id in  ('RegionalCollectionsHead');
update Designation set Level = 6 where Id in  ('ZonalCollectionsHead');
update Designation set Level = 7 where Id in  ('CountryCollectionsHead');







INSERT INTO `UserLevelProjection`
    (`Id`, `ApplicationUserId`, `Level`, `CreatedDate`, `LastModifiedDate`, `IsDeleted`)
SELECT
    -- GUID without dashes or tildes
    REPLACE(UUID(), '-', '') AS `Id`,
    sub.`ApplicationUserId`,
    sub.`Level`,
    NOW() AS `CreatedDate`,
    NOW() AS `LastModifiedDate`,
    0 AS `IsDeleted`
FROM
(
    -- distinct user+level combinations
    SELECT DISTINCT
        cud.`CompanyUserId`       AS `ApplicationUserId`,
        d.`Level`
    FROM CompanyUserDesignation AS cud
    INNER JOIN `Designation` AS d
        ON cud.`DesignationId` = d.`Id`
    WHERE d.`IsDeleted` = 0
) AS sub;
