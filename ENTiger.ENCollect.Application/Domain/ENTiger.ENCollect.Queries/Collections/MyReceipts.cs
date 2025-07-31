using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class MyReceipts : FlexiQueryPagedListBridgeAsync<Collection, MyReceiptsParams, MyReceiptsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MyReceipts> _logger;
        protected MyReceiptsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MyReceipts(ILogger<MyReceipts> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MyReceipts AssignParameters(MyReceiptsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<MyReceiptsDto>> Fetch()
        {
            var projection = await Build<Collection>().SelectTo<MyReceiptsDto>().ToListAsync();

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
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            string userId = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCollectionCollectorId(userId);

            if (_params.FromDate != null && _params.ToDate != null)
            {
                query = query.CollectionDateRange(_params.FromDate, _params.ToDate);
            }
            else
            {
                query = query.WithMonthAndYear(_params.Month, _params.year);
            }
            query = query.OrderByDescending(a => a.CreatedDate);
            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class MyReceiptsParams : PagedQueryParamsDtoBridge
    {
        public long Month { get; set; }

        public long year { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}