using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class GetCustomerConsentListToExpire : FlexiQueryEnumerableBridgeAsync<CustomerConsent, GetCustomerConsentListToExpireDto>
    {

        protected readonly ILogger<GetCustomerConsentListToExpire> _logger;
        protected GetCustomerConsentListToExpireParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCustomerConsentListToExpire(ILogger<GetCustomerConsentListToExpire> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCustomerConsentListToExpire AssignParameters(GetCustomerConsentListToExpireParams @params)
        {
            _params = @params;
            return this;
        }

        public async override Task<IEnumerable<GetCustomerConsentListToExpireDto>> Fetch()
        {
            var result = await Build<CustomerConsent>().SelectTo<GetCustomerConsentListToExpireDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                         .ByDateToExpire(_params.ExpiryDate)
                                         ;

            return query;
        }
    }

    public class GetCustomerConsentListToExpireParams : DtoBridge
    {
        public DateTime? ExpiryDate { get; set; } = DateTime.Now;
    }
}
