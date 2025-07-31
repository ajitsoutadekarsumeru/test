using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBankBranchByBankId : FlexiQueryEnumerableBridgeAsync<BankBranch, GetBankBranchByBankIdDto>
    {
        protected readonly ILogger<GetBankBranchByBankId> _logger;
        protected GetBankBranchByBankIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetBankBranchByBankId(ILogger<GetBankBranchByBankId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetBankBranchByBankId AssignParameters(GetBankBranchByBankIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetBankBranchByBankIdDto>> Fetch()
        {
            var result = await Build<BankBranch>().SelectTo<GetBankBranchByBankIdDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(x => x.BankId == _params.Id);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetBankBranchByBankIdParams : DtoBridge
    {
        public string? Id { get; set; }
    }
}