INSERT INTO HierarchyLevel (Id,Name,[Order],Type,CreatedDate,LastModifiedDate,IsDeleted)
VALUES 
('HL01','ProductGroup',1, 'Product', NOW(), NOW(),0),
('HL02','Product',2, 'Product', NOW(), NOW(),0),
('HL03','SubProduct',3, 'Product', NOW(), NOW(),0),
('HL04','Level1',1, 'Geo', NOW(), NOW(),0),
('HL05','Level2',2, 'Geo', NOW(), NOW(),0),
('HL06','Level3',3, 'Geo', NOW(), NOW(),0),
('HL07','Level4',4, 'Geo', NOW(), NOW(),0),
('HL08','Level5',5, 'Geo', NOW(), NOW(),0);


INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT Id,FirstName,'HL08',NULL,NULL,NOW(),NULL,NOW(),0
from applicationorg
where Discriminator = 'BaseBranch';

INSERT INTO AccountGeoMap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
select replace(UUID(),'-',''),l.Id,hm.Id,1,NULL,NOW(),NULL,NOW(),0
from LoanAccounts l
	inner join HierarchyMaster hm ON L.Branch = HM.Item;

-- country
INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),Country,'HL04',NULL,NULL,NOW(),NULL,NOW(),0
FROM geoMaster
GROUP BY Country;

INSERT INTO AccountGeoMap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,(select Id from HierarchyMaster where LevelId = 'HL04' and Item = 'INDIA'),5,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l;
-- region

INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),Region,'HL05',hm.Id,NULL,NOW(),NULL,NOW(),0
FROM geoMaster gm
	inner join HierarchyMaster hm on gm.country = hm.Item AND hm.LevelId = 'HL04'
GROUP BY Region,hm.Id;

INSERT INTO AccountGeoMap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,4,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.Region = HM.Item and HM.LevelId = 'HL05';

-- state

INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),State,'HL06',hm.Id,NULL,NOW(),NULL,NOW(),0
FROM geoMaster gm
	inner join HierarchyMaster hm on gm.Region = hm.Item AND hm.LevelId = 'HL05'
GROUP BY State,hm.Id;

INSERT INTO AccountGeoMap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,3,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.State = HM.Item and HM.LevelId = 'HL06';

-- city

INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),city,'HL07',hm.Id,NULL,NOW(),NULL,NOW(),0
FROM geoMaster gm
	INNER JOIN HierarchyMaster hm on gm.State = hm.Item AND hm.LevelId = 'HL06'
GROUP BY city,hm.Id;

INSERT INTO AccountGeoMap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,2,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.City = HM.Item and HM.LevelId = 'HL07';

-- branch parent update
UPDATE hm
SET hm.ParentId = phm.Id
FROM HierarchyMaster hm
JOIN geoMaster gm ON hm.Id = gm.BaseBranchId
JOIN HierarchyMaster phm ON gm.city = phm.Item AND phm.LevelId = 'HL07'
WHERE hm.LevelId = 'HL08';


-- product group
INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),Name,'HL01',NULL,NULL,NOW(),NULL,NOW(),0
FROM categoryitem ci
where CategoryMasterId = 'ProductGroup'
GROUP BY Name
ORDER BY Name;

INSERT INTO accountproductmap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,1,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.ProductGroup = HM.Item AND HM.IsDeleted = 0
WHERE HM.LevelId = 'HL01';

--product
INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),ci.Name,'HL02',phm.Id,NULL,NOW(),NULL,NOW(),0
FROM categoryitem ci
	INNER JOIN categoryitem cip on ci.ParentId = cip.Id
    INNER JOIN HierarchyMaster phm ON cip.Name = phm.Item 
where ci.CategoryMasterId = 'Product' and phm.LevelId = 'HL01'
GROUP BY ci.Name,phm.Id
ORDER BY ci.Name;

INSERT INTO accountproductmap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,2,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.product = HM.Item AND HM.IsDeleted = 0
WHERE HM.LevelId = 'HL02';

-- sub product
INSERT INTO HierarchyMaster (Id,Item,LevelId,ParentId,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),ci.Name,'HL03',phm.Id,NULL,NOW(),NULL,NOW(),0
FROM categoryitem ci
	INNER JOIN categoryitem cip on ci.ParentId = cip.Id
    INNER JOIN HierarchyMaster phm ON cip.Name = phm.Item 
where ci.CategoryMasterId = 'SubProduct' and phm.LevelId = 'HL02'
GROUP BY ci.Name,phm.Id
ORDER BY ci.Name;

INSERT INTO accountproductmap (Id,AccountId,HierarchyId,HierarchyLevel,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,IsDeleted)
SELECT REPLACE(UUID(),'-',''),l.Id,HM.Id,3,NULL,NOW(),NULL,NOW(),0
FROM LoanAccounts l
	INNER JOIN HierarchyMaster HM ON L.subproduct = HM.Item AND HM.IsDeleted = 0
WHERE HM.LevelId = 'HL03';
