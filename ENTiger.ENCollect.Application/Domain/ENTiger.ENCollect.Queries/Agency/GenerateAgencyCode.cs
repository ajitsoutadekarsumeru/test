using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GenerateAgencyCode : FlexiQueryBridgeAsync<Agency, GenerateAgencyCodeDto>
    {
        protected readonly ILogger<GenerateAgencyCode> _logger;
        protected GenerateAgencyCodeParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ICustomUtility _customUtility;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GenerateAgencyCode(ILogger<GenerateAgencyCode> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GenerateAgencyCode AssignParameters(GenerateAgencyCodeParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GenerateAgencyCodeDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line

            //var result = Build<Agency>().SelectTo<GenerateAgencyCodeDto>().FirstOrDefaultAsync();
            GenerateAgencyCodeDto result = new GenerateAgencyCodeDto();
            result.agencycode = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.Agency.Value);

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

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GenerateAgencyCodeParams : DtoBridge
    {
    }
}