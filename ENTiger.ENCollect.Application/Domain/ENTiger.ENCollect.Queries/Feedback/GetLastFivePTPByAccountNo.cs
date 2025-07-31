using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetLastFivePTPByAccountNo : FlexiQueryEnumerableBridgeAsync<Feedback, GetLastFivePTPByAccountNoDto>
    {
        protected readonly ILogger<GetLastFivePTPByAccountNo> _logger;
        protected GetLastFivePTPByAccountNoParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string? AccountId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetLastFivePTPByAccountNo(ILogger<GetLastFivePTPByAccountNo> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetLastFivePTPByAccountNo AssignParameters(GetLastFivePTPByAccountNoParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetLastFivePTPByAccountNoDto>> Fetch()
        {
            _repoFactory.Init(_params);
            AccountId = await FetchAccountIdASync(_params.accountno);

            var result = await Build<Feedback>().SelectTo<GetLastFivePTPByAccountNoDto>().ToListAsync();

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
                                 .Where(a => !string.IsNullOrEmpty(a.DispositionCode) &&
                                          (string.Equals(a.DispositionCode, DispCodeEnum.PTP.Value) ||
                                           string.Equals(a.DispositionCode, DispCodeEnum.BPTP.Value)))
                                 .OrderByDescending(fd => fd.FeedbackDate)
                                 .Take(5);

            return query;
        }

        private async Task<string?> FetchAccountIdASync(string accountNo)
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
    public class GetLastFivePTPByAccountNoParams : DtoBridge
    {
        public string? accountno { get; set; }
    }
}