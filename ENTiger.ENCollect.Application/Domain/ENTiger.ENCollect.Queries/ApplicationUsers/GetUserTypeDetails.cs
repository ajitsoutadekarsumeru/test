
namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class GetUserTypeDetails : FlexiQueryBridgeAsync<ApplicationUser, GetUserTypeDetailsDto>
    {
        protected readonly ILogger<GetUserTypeDetails> _logger;
        protected GetUserTypeDetailsParams _params;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;
        private string userId, tenantId;
        private readonly ILicenseService _licenseService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUserTypeDetails(ILogger<GetUserTypeDetails> logger, IRepoFactory repoFactory, ILicenseService licenseService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUserTypeDetails AssignParameters(GetUserTypeDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///  Retrieves licensing detail for supplied user type
        /// </summary>
        /// <returns></returns>
        public override async Task<GetUserTypeDetailsDto> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;
            tenantId = _flexAppContext.TenantId;

            GetUserTypeDetailsDto result = new GetUserTypeDetailsDto();
            UserTypeEnum userType = Enum.TryParse(_params.UserType, out UserTypeEnum enumResult) ? enumResult : UserTypeEnum.Unknown;
            var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType, _params);

            if (validateResult != null)
            {
                result.CurrentConsumption = validateResult.ActualCount;
                result.Limit = validateResult.PermittedCount;
                decimal used = (result.CurrentConsumption / (result.Limit == 0 ? 1 : result.Limit)) * 100;
                result.PercentUsed = Math.Floor(used);
                result.ColourCode = await _licenseService.GetUtilizationColor(result.PercentUsed);
            }

            return result;
        }


        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            return query;
        }
    }

    public class GetUserTypeDetailsParams : DtoBridge
    {
        public string? UserType { get; set; }
    }
}