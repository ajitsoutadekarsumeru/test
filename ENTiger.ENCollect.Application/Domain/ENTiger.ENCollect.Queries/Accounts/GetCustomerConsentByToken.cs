using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class GetCustomerConsentByToken : FlexiQueryBridgeAsync<CustomerConsent, GetCustomerConsentByTokenDto>
    {
        protected readonly ILogger<GetCustomerConsentByToken> _logger;
        protected GetCustomerConsentByTokenParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCustomerConsentByToken(ILogger<GetCustomerConsentByToken> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetCustomerConsentByToken AssignParameters(GetCustomerConsentByTokenParams @params)
        {
            _params = @params;
            return this;
        }

        public async override Task<GetCustomerConsentByTokenDto> Fetch()
        {
            var result = await Build<CustomerConsent>().SelectTo<GetCustomerConsentByTokenDto>().FirstOrDefaultAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.SecureToken == _params.Token);
            return query;
        }
    }

    public class GetCustomerConsentByTokenParams : DtoBridge
    {
        public string Token { get; set; }
    }
}

