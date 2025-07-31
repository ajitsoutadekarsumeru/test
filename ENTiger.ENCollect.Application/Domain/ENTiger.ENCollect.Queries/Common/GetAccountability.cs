using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAccountability : FlexiQueryBridgeAsync<GetAccountabilityDto>
    {
        protected readonly ILogger<GetAccountability> _logger;
        protected GetAccountabilityParams _params;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetAccountability(ILogger<GetAccountability> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetAccountability AssignParameters(GetAccountabilityParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetAccountabilityDto> Fetch()
        {
            _repoFactory.Init(_params);

            // Fetch the Department object based on the DepartmentId parameter
            var departmentType = await _repoFactory.GetRepo().FindAll<Department>().Where(x => x.Id == _params.DepartmentId)
                                        .Select(a => a.DepartmentTypeId).FirstOrDefaultAsync();

            // Fetch the Designation object based on the DesignationId parameter
            var designationType = await _repoFactory.GetRepo().FindAll<Designation>().Where(x => x.Id == _params.DesignationId)
                                        .Select(a => a.DesignationTypeId).FirstOrDefaultAsync();

            string type = departmentType.ToLower();
            string accountability = (type.Contains("external") ? "AgencyTo" : type.Contains("internal") ? "BankTo" : "") + departmentType + designationType;
            // Create and return the accountability output model
            GetAccountabilityDto result = new GetAccountabilityDto
            {
                Accountability = accountability
            };

            return result;
        }
    }

    public class GetAccountabilityParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        [Required]
        public string? DepartmentId { get; set; }

        [Required]
        public string? DesignationId { get; set; }
    }
}