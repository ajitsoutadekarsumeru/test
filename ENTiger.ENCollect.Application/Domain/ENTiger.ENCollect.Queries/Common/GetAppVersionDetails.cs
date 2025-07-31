using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAppVersionDetails : FlexiQueryBridgeAsync<CategoryItem, GetAppVersionDetailsDto>
    {
        protected readonly ILogger<GetAppVersionDetails> _logger;
        protected GetAppVersionDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly MobileSettings _mobileSettings;
        private string _catergoryMasterId = string.Empty;
        private string _appURL = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAppVersionDetails(ILogger<GetAppVersionDetails> logger, IRepoFactory repoFactory, IOptions<MobileSettings> mobileSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _mobileSettings = mobileSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAppVersionDetails AssignParameters(GetAppVersionDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetAppVersionDetailsDto> Fetch()
        {
            _catergoryMasterId = _mobileSettings.AppVersion;
            _appURL = _mobileSettings.AppUrl;
            GetAppVersionDetailsDto result = new GetAppVersionDetailsDto();
            var output = await Build<CategoryItem>().FirstOrDefaultAsync();
            if (output != null)
            {
                result.IsVersionCheck = true;
                result.Message = "Version is available.";
                result.AppUrl = _appURL;
            }
            else
            {
                result.IsVersionCheck = false;
                result.Message = "Version is not available.";
                result.AppUrl = _appURL;
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCategoryNameAndMasterId(_params.VersionName, _catergoryMasterId)
                                    .ByNotDeleteCategoryItem();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAppVersionDetailsParams : DtoBridge
    {
        public string VersionName { get; set; }
    }
}