using AutoMapper;
using AutoMapper.QueryableExtensions;
using Babel.Licensing;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.CollectionsModule;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserTypeUsageDetails : FlexiQueryBridgeAsync<GetUserTypeUsageDetailsDto>
    {
        protected readonly ILogger<GetUserTypeUsageDetails> _logger;
        protected GetUserTypeUsageDetailsParams _params;
        private readonly RepoFactory _repoFactory;
        private readonly ILicenseService _licenseService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetUserTypeUsageDetails(ILogger<GetUserTypeUsageDetails> logger, RepoFactory repoFactory, ILicenseService licenseService)
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
        public virtual GetUserTypeUsageDetails AssignParameters(GetUserTypeUsageDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<GetUserTypeUsageDetailsDto> Fetch()
        {
            GetUserTypeUsageDetailsDto result = new GetUserTypeUsageDetailsDto();

            UserTypeEnum userType = Enum.TryParse(_params.UserType, out UserTypeEnum enumResult) ? enumResult : UserTypeEnum.Unknown;
            var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType, _params);
            
            if (validateResult != null)
            {
                result.CurrentConsumption = validateResult.ActualCount;
                result.Limit = validateResult.PermittedCount;
                decimal used = ((decimal)result.CurrentConsumption / (result.Limit == 0 ? 1 : result.Limit)) * 100;
                result.PercentUsed = Math.Floor(used);
                result.ColourCode = await _licenseService.GetUtilizationColor(result.PercentUsed);
            }

            return result ?? new GetUserTypeUsageDetailsDto();
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class GetUserTypeUsageDetailsParams : DtoBridge
    {
        public string? UserType { get; set; }
    }
}
