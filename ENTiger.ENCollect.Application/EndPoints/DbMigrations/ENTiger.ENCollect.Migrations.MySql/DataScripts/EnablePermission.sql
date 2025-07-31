INSERT INTO `enabledpermission` (
	`Id`
	,`PermissionId`
	,`PermissionSchemeId`
	,`CreatedBy`
	,`CreatedDate`
	,`LastModifiedBy`
	,`LastModifiedDate`
	,`IsDeleted`
	)
SELECT REPLACE(UUID(), '-', '')
	,Id
	,'AdministratorScheme'
	,NULL
	,now()
	,NULL
	,now()
	,0
FROM permissions



