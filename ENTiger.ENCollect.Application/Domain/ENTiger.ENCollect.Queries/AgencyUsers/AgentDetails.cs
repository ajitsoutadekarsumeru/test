using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class AgentDetails : FlexiQueryBridgeAsync<AgencyUser, AgentDetailsDto>
    {
        protected readonly ILogger<AgentDetails> _logger;
        protected AgentDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public AgentDetails(ILogger<AgentDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual AgentDetails AssignParameters(AgentDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<AgentDetailsDto> Fetch()
        {
            //TODO : Refactor to finish the work with 1 DB call
            var model = await Build<AgencyUser>().FirstOrDefaultAsync();

            var result = await Build<AgencyUser>().SelectTo<AgentDetailsDto>().FirstOrDefaultAsync();

            result.IsPrintValid = false;

            var workFlowStates = await _repoFactory.GetRepo().FindAllObjectWithState<AgencyUserWorkflowState>()
                                    .Where(a => a.TFlexId == result.Id)
                                    .OrderByDescending(a => a.StateChangedDate)
                                    .SelectTo<AgentChangeLogInfoDto>().ToListAsync();

            var userIds = workFlowStates.Select(x => x.ChangedByUserId).Distinct().ToArray();

            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var state in workFlowStates)
            {
                var stateChangedUser = users.Where(x => x.Id == state.ChangedByUserId).FirstOrDefault();
                state.ChangedByUserName = stateChangedUser != null ? stateChangedUser.FirstName + " " + stateChangedUser.LastName : string.Empty;
            }
            result.ChangeLogInfo = workFlowStates;

            if (model.CreditAccountDetails != null && model.CreditAccountDetails.BankBranch != null)
            {
                result.CreditAccountDetails.BankId = model.CreditAccountDetails?.BankBranch?.BankId;
                result.CreditAccountDetails.BankName = model.CreditAccountDetails?.BankBranch?.Bank?.Name;
                result.CreditAccountDetails.IfscCode = model.CreditAccountDetails.BankBranch.Code;
                result.CreditAccountDetails.BankBranchId = model.CreditAccountDetails.BankBranch.Id;
                result.CreditAccountDetails.BankBranchName = model.CreditAccountDetails.BankBranch.Name;
                result.CreditAccountDetails.MICR = model.CreditAccountDetails.BankBranch.MICR;
            }

            result.ProfileIdentification = new List<ProfileIdentificationDto>();
            foreach (AgencyUserIdentification identification in model.AgencyUserIdentifications)
            {
                ProfileIdentificationDto ProfileIdentification = new ProfileIdentificationDto();
                ProfileIdentification.Id = identification.Id;
                ProfileIdentification.IdentificationTypeId = identification.TFlexIdentificationTypeId;
                ProfileIdentification.IdentificationDocTypeId = identification.TFlexIdentificationDocTypeId;
                ProfileIdentification.DeferredTillDate = identification.DeferredTillDate;
                ProfileIdentification.IsDeferred = identification.IsDeferred;
                ProfileIdentification.IsWavedOff = identification.IsWavedOff;
                ProfileIdentification.TflexId = identification.TFlexId;
                ProfileIdentification.IsDelete = identification.IsDeleted;

                ProfileIdentification.IdentificationDocId = new List<ProfileIdentificationDocDto>();
                foreach (AgencyUserIdentificationDoc doc in identification.TFlexIdentificationDocs)
                {
                    ProfileIdentificationDocDto Document = new ProfileIdentificationDocDto()
                    {
                        Id = doc.Id,
                        FileName = doc.FileName
                    };
                    ProfileIdentification.IdentificationDocId.Add(Document);
                }
                result.ProfileIdentification.Add(ProfileIdentification);
            }

            var designations = await _repoFactory.GetRepo().FindAll<Designation>()
                                                .Where(a => result.Roles.Select(r => r.DesignationId).Contains(a.Id))
                                                .Select(x => new { x.Id, x.Name })
                                                .ToDictionaryAsync(x => x.Id, x => x.Name);

            foreach (var role in result.Roles)
            {
                if (designations.TryGetValue(role.DesignationId, out var designationName))
                {
                    role.DesignationName = designationName;
                }
                else
                {
                    role.DesignationId = null; // Or some default value
                }
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id)
                                        .IncludeAgencyUserIdentifications()
                                        .IncludeAgencyUserIdentificationDocs()
                                        .IncludeAgencyUserBankBranch()
                                        .IncludeAgencyUserBank()
                                        .IncludeAgencyUserCreditAccountDetails()
                                        .IncludeWallet()
                                        .IncludeUserProductScope()
                                        .IncludeUserGeoScope()
                                        .IncludeUserBucketScope();
            return query;
        }
    }

    public class AgentDetailsParams : DtoBridge
    {
        public string Id { get; set; }
    }
}