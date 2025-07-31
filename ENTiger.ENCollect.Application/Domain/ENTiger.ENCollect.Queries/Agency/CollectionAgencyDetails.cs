using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class CollectionAgencyDetails : FlexiQueryBridgeAsync<Agency, CollectionAgencyDetailsDto>
    {
        protected readonly ILogger<CollectionAgencyDetails> _logger;
        protected CollectionAgencyDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public CollectionAgencyDetails(ILogger<CollectionAgencyDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual CollectionAgencyDetails AssignParameters(CollectionAgencyDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<CollectionAgencyDetailsDto> Fetch()
        {
            var result = await Build<Agency>().SelectTo<CollectionAgencyDetailsDto>().FirstOrDefaultAsync();
             
            result.PAN = Convert.ToBase64String(Encoding.UTF8.GetBytes(result.PAN));                             
            
            var workFlowStates = await _repoFactory.GetRepo().FindAllObjectWithState<AgencyWorkflowState>()
                                    .Where(a => a.TFlexId == result.Id)
                                    .OrderByDescending(a => a.StateChangedDate)
                                    .SelectTo<AgencyChangeLogInfoDto>().ToListAsync();

            var userIds = workFlowStates.Select(x => x.ChangedByUserId).Distinct().ToArray();

            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var state in workFlowStates)
            {
                var stateChangedUser = users.Where(x => x.Id == state.ChangedByUserId).FirstOrDefault();
                state.ChangedByUserName = stateChangedUser != null ? stateChangedUser.FirstName + " " + stateChangedUser.LastName : string.Empty;
            }
            result.ChangeLogInfo = workFlowStates;
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
                                    .IncludeCreditAccountDetails()
                                    .IncludeAgencyBankBranch()
                                    .IncludeAgencyBank();
            return query;
        }
    }

    public class CollectionAgencyDetailsParams : DtoBridge
    {
        public string Id { get; set; }
    }
}