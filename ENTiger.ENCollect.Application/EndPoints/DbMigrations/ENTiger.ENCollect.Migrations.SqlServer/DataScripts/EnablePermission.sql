INSERT INTO [enabledpermission] (
	[Id]
	,[PermissionId]
	,[PermissionSchemeId]
	,[CreatedBy]
	,[CreatedDate]
	,[LastModifiedBy]
	,[LastModifiedDate]
	,[IsDeleted]
	)
SELECT REPLACE(UUID(), '-', '')
	,Id
	,'AdministratorScheme'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0
FROM permissions