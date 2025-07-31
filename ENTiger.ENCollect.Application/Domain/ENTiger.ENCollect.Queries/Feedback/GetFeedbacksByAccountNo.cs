using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetFeedbacksByAccountNo : FlexiQueryPagedListBridgeAsync<Feedback, GetFeedbacksByAccountNoParams, GetFeedbacksByAccountNoDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetFeedbacksByAccountNo> _logger;
        protected GetFeedbacksByAccountNoParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly SystemUserSettings _autoTrailSettings;
        private string? AccountId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetFeedbacksByAccountNo(ILogger<GetFeedbacksByAccountNo> logger, IRepoFactory repoFactory, IOptions<SystemUserSettings> autoTrailSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _autoTrailSettings = autoTrailSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetFeedbacksByAccountNo AssignParameters(GetFeedbacksByAccountNoParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetFeedbacksByAccountNoDto>> Fetch()
        {
            _repoFactory.Init(_params);
            AccountId = await FetchAccountIdAsync(_params.accountno);

            var projection = await Build<Feedback>().SelectTo<GetFeedbacksByAccountNoDto>().ToListAsync();

            var result = BuildPagedOutput(projection);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                .ByFeedbackAccountId(AccountId)
                .ByExcludeAutoTrail(_autoTrailSettings.SystemUserId)
                .OrderByDescending(fd => fd.FeedbackDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }

        private async Task<string?> FetchAccountIdAsync(string accountNo)
        {
            return await _repoFactory.GetRepo()
                            .FindAll<LoanAccount>()
                            .ByAccountNo(accountNo)
                            .Select(a => a.Id)
                            .FirstOrDefaultAsync();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetFeedbacksByAccountNoParams : PagedQueryParamsDtoBridge
    {
        public string? accountno { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}