using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class BaseBranchById : FlexiQueryBridgeAsync<BaseBranch, BaseBranchByIdDto>
    {
        protected readonly ILogger<BaseBranchById> _logger;
        protected BaseBranchByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public BaseBranchById(ILogger<BaseBranchById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual BaseBranchById AssignParameters(BaseBranchByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<BaseBranchByIdDto> Fetch()
        {
            var result = await Build<BaseBranch>().SelectTo<BaseBranchByIdDto>().FirstOrDefaultAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id).ByNotDeletedBaseBranch();
            return query;
        }
    }

    public class BaseBranchByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}