using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetPaymentStatus : FlexiQueryEnumerableBridgeAsync<CategoryItem, GetPaymentStatusDto>
    {
        protected readonly ILogger<GetPaymentStatus> _logger;
        protected GetPaymentStatusParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string? paymentstatusCategoryMasterID;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetPaymentStatus(ILogger<GetPaymentStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetPaymentStatus AssignParameters(GetPaymentStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetPaymentStatusDto>> Fetch()
        {
            _repoFactory.Init(_params);

            paymentstatusCategoryMasterID = await _repoFactory.GetRepo().FindAll<CategoryMaster>()
                                                .Where(a => string.Equals(a.Name, "paymentstatusvalues"))
                                                .Select(s => s.Id)
                                                .FirstOrDefaultAsync();

            var result = await Build<CategoryItem>().SelectTo<GetPaymentStatusDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.CategoryMasterId == paymentstatusCategoryMasterID);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetPaymentStatusParams : DtoBridge
    {
    }
}