namespace ENTiger.ENCollect
{
    public class PermissionService : IPermissionService
    {
        protected readonly IRepoFactory _repoFactory;
        private readonly DatabaseSettings _databaseSettings;
        public PermissionService(IRepoFactory repoFactory, IOptions<DatabaseSettings> databaseSettings)
        {
            _repoFactory = repoFactory;
            _databaseSettings = databaseSettings.Value;
        }

        public List<Permissions> GetPermissions()
        {
            FlexAppContextBridge context = new FlexAppContextBridge() 
            { 
                TenantId = _databaseSettings.DefaultTenant
            };
            _repoFactory.Init(context);
            return _repoFactory.GetRepo().FindAll<Permissions>().ToList();
        }
    }
}
