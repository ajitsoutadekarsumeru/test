using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUsersByAccountability : FlexiQueryEnumerableBridgeAsync<GetUsersByAccountabilityDto>
    {
        protected readonly ILogger<GetUsersByAccountability> _logger;
        protected GetUsersByAccountabilityParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string _UserId;
        private List<string> _accountabilityType;
        private string _userbranchId;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersByAccountability(ILogger<GetUsersByAccountability> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByAccountability AssignParameters(GetUsersByAccountabilityParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByAccountabilityDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string userid = _flexAppContext.UserId;
            List<GetUsersByAccountabilityDto> result = new List<GetUsersByAccountabilityDto>();

            List<string> accountabilitytypeid = null;

            List<Accountability> accountabilitylist = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == userid).ToListAsync();

            if (accountabilitylist != null)
            {
                accountabilitytypeid = accountabilitylist.Select(x => x.AccountabilityTypeId.ToLower()).ToList();
                _accountabilityType = accountabilitytypeid;
            }

            var companyuser =await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(a => a.Id == userid).FirstOrDefaultAsync();
            if (companyuser != null)
            {
                _userbranchId = companyuser.BaseBranchId;
            }

            string accountability = _params.accountType;

            List<string> responsibleIds = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.AccountabilityTypeId == accountability)
                                                     .Select(x => x.ResponsibleId).ToListAsync();
            //.ByAccountType(accountability);

            AccountabilityType accountabilityType = await _repoFactory.GetRepo().FindAll<AccountabilityType>().Where(a => a.Id == accountability).FirstOrDefaultAsync();
            string Description = accountabilityType.Description;

            if (_accountabilityType.Contains("bihp"))
            {
                result = await (_repoFactory.GetRepo().FindAll<AgencyUser>().Where(a => responsibleIds.Contains(a.Id))).Select(p => new
                {
                    p.Id,
                    p.FirstName,
                    p.LastName,
                    p.PrimaryEMail,
                    p.PrimaryMobileNumber,
                    p.Designation
                }).Select(a => new GetUsersByAccountabilityDto
                {
                    userId = a.Id,
                    Name = a.FirstName + " " + a.LastName,
                    EmailId = a.PrimaryEMail,
                    Mobile = a.PrimaryMobileNumber,
                    ResponsibilityInfos = new List<ResponsibilityInfo>
                                                {
                                                    new ResponsibilityInfo{
                                                    Id = a.Id,
                                                    Role = accountability,
                                                    Description= Description.ToString()
                                                    }
                                                }.ToList(),
                    Roles = a.Designation.Select(d => new UserDesignationOutputApiModel
                    {
                        Id = d.Id,
                        DepartmentId = d.Department.Name,
                        DesignationId = d.Designation.Name,
                    }).ToList()
                })
               .ToListAsync();
            }
            else
            {
                result = await (_repoFactory.GetRepo().FindAll<CompanyUser>()
                                      .Where(a => responsibleIds.Contains(a.Id)))
                                      .FlexInclude(x => x.Designation)
                                      .Where(x => x.BaseBranchId == _userbranchId)
                                      .Select(p => new
                                      {
                                          p.Id,
                                          p.FirstName,
                                          p.LastName,
                                          p.PrimaryEMail,
                                          p.PrimaryMobileNumber,
                                          p.Designation,
                                      }).Select(a => new GetUsersByAccountabilityDto
                                      {
                                          userId = a.Id,
                                          Name = a.FirstName + " " + a.LastName,
                                          EmailId = a.PrimaryEMail,
                                          Mobile = a.PrimaryMobileNumber,
                                          ResponsibilityInfos = new List<ResponsibilityInfo>
                                            {
                                                    new ResponsibilityInfo{
                                                    Id = a.Id,
                                                    Role = accountability,
                                                    Description= Description.ToString()
                                                    }
                                            }.ToList(),
                                          Roles = a.Designation.Select(d => new UserDesignationOutputApiModel
                                          {
                                              Id = d.Id,
                                              DepartmentId = d.Department.Name,
                                              DesignationId = d.Designation.Name,
                                          }).ToList()
                                      }).ToListAsync();
            }

            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUsersByAccountabilityParams : DtoBridge
    {
        public string? accountType { get; set; }
    }
}