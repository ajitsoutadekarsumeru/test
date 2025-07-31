INSERT INTO [permissionschemes] (
    [Id],
    [Name],
    [Remarks],
    [CreatedBy],
    [CreatedDate],
    [LastModifiedBy],
    [LastModifiedDate],
    [IsDeleted]
)
SELECT 'AdministratorScheme'
	,'Administrator Scheme'
	,'Scheme forAdministrator Level Permissions'
	,NULL
	,GETDATE()
	,NULL
	,GETDATE()
	,0;
