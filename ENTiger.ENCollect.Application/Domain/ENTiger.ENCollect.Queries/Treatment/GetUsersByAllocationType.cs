using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUsersByAllocationType : FlexiQueryEnumerableBridgeAsync<GetUsersByAllocationTypeDto>
    {
        protected readonly ILogger<GetUsersByAllocationType> _logger;
        protected GetUsersByAllocationTypeParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersByAllocationType(ILogger<GetUsersByAllocationType> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByAllocationType AssignParameters(GetUsersByAllocationTypeParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByAllocationTypeDto>> Fetch()
        {
            _repoFactory.Init(_params);
            IEnumerable<GetUsersByAllocationTypeDto> result = new List<GetUsersByAllocationTypeDto>();

            switch (_params.Type.ToLower())
            {
                case "telecallingagency":
                    result = await FetchTCAgencies();
                    return result;

                case "fieldagency":
                    result = await FetchFieldAgencies();
                    return result;

                case "bankstaff":
                    result = await FetchCompanyUsers();
                    return result;

                case "allocationowner":
                    result = await FetchCompanyUsers();
                    return result;

                case "tcagent":

                    result = await FetchTCAgents();
                    return result;

                case "fieldagent":

                    result = await FetchFieldAgents();
                    return result;
            }
            return result;
        }

        private async Task<List<GetUsersByAllocationTypeDto>> FetchFieldAgents()
        {
            List<GetUsersByAllocationTypeDto> output;

            output = await _repoFactory.GetRepo().FindAll<AgencyUser>().FlexInclude(a => a.AgencyUserWorkflowState)
                        .Where(a => a.AgencyUserWorkflowState != null && string.Equals(a.AgencyUserWorkflowState.Name, "AgencyUserApproved"))
                    .Select(a => new GetUsersByAllocationTypeDto
                    {
                        Id = a.Id,
                        Name = a.FirstName + " " + a.LastName
                    })
                    .OrderBy(a => a.Name)
                    .ToListAsync();

            List<string> userids = output.Select(a => a.Id).ToList();

            var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                    .Where(a => userids.Contains(a.ResponsibleId)
                                    && string.Equals(a.AccountabilityTypeId, AccountabilityTypeEnum.AgencyToFrontEndExternalFOS.Value)).ToListAsync();

            List<string> responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

            output = output.Where(a => responsibleids.Contains(a.Id)).ToList();

            return output;

        }

        private async Task<List<GetUsersByAllocationTypeDto>> FetchTCAgents()
        {
            List<GetUsersByAllocationTypeDto> output;
            List<string> responsibleids = new List<string>();

            output = await _repoFactory.GetRepo().FindAll<AgencyUser>().FlexInclude(a => a.AgencyUserWorkflowState)
                        .Where(a => a.AgencyUserWorkflowState != null && string.Equals(a.AgencyUserWorkflowState.Name, "AgencyUserApproved"))
                      .Select(a => new GetUsersByAllocationTypeDto
                      {
                          Id = a.Id,
                          Name = a.FirstName + " " + a.LastName
                      })
                      .OrderBy(a => a.Name)
                      .ToListAsync();

            List<string> userids = output.Select(a => a.Id).ToList();

            var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                .Where(a => userids.Contains(a.ResponsibleId) &&
                            string.Equals(a.AccountabilityTypeId, AccountabilityTypeEnum.AgencyToFrontEndExternalTC.Value))
                .ToListAsync();

            responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

            output = output.Where(a => responsibleids.Contains(a.Id)).ToList();

            return output;

        }

        private async Task<List<GetUsersByAllocationTypeDto>> FetchCompanyUsers()
        {
            List<GetUsersByAllocationTypeDto> output;

            output = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                     .FlexInclude(a => a.CompanyUserWorkflowState)
                     .Where(a => a.CompanyUserWorkflowState != null &&
                                 string.Equals(a.CompanyUserWorkflowState.Name, "CompanyUserApproved"))
                     .Select(a => new GetUsersByAllocationTypeDto
                     {
                         Id = a.Id,
                         Name = a.FirstName + " " + a.LastName
                     })
                     .OrderBy(a => a.Name)
                     .ToListAsync();

            return output;

        }

        private async Task<List<GetUsersByAllocationTypeDto>> FetchFieldAgencies()
        {
            List<GetUsersByAllocationTypeDto> output;

            output = await _repoFactory.GetRepo().FindAll<Agency>()
                     .FlexInclude(a => a.AgencyWorkflowState)
                     .FlexInclude(a => a.AgencyType)
                     .Where(a => a.AgencyWorkflowState != null &&
                                 string.Equals(a.AgencyWorkflowState.Name, "AgencyApproved") &&
                                 string.Equals(a.AgencyType.SubType.Trim(), AgencySubTypeEnum.FieldAgent.Value))
                     .Select(a => new GetUsersByAllocationTypeDto
                     {
                         Id = a.Id,
                         Name = a.FirstName
                     })
                     .OrderBy(a => a.Name)
                     .ToListAsync();

            return output;

        }

        private async Task<List<GetUsersByAllocationTypeDto>> FetchTCAgencies()
        {
            List<GetUsersByAllocationTypeDto> output;

            output = await _repoFactory.GetRepo().FindAll<Agency>()
                     .FlexInclude(a => a.AgencyWorkflowState)
                     .FlexInclude(a => a.AgencyType)
                     .Where(a => a.AgencyWorkflowState != null &&
                                 string.Equals(a.AgencyWorkflowState.Name, "AgencyApproved") &&
                                 a.AgencyType != null &&
                                 string.Equals(a.AgencyType.SubType, AgencySubTypeEnum.TeleCalling.Value))
                     .Select(a => new GetUsersByAllocationTypeDto
                     {
                         Id = a.Id,
                         Name = a.FirstName
                     })
                     .OrderBy(a => a.Name)
                     .ToListAsync();
            return output;

        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUsersByAllocationTypeParams : DtoBridge
    {
        public string Type { get; set; }
    }
}