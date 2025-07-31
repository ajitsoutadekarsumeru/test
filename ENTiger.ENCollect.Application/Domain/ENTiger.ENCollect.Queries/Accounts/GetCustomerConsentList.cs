using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public class GetCustomerConsentList : FlexiQueryEnumerableBridgeAsync<CustomerConsent, GetCustomerConsentListDto>
    {
        
        protected readonly ILogger<GetCustomerConsentList> _logger;
        protected GetCustomerConsentListParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCustomerConsentList(ILogger<GetCustomerConsentList> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCustomerConsentList AssignParameters(GetCustomerConsentListParams @params)
        {
            _params = @params;
            return this;
        }

        public async override Task<IEnumerable<GetCustomerConsentListDto>> Fetch()
        {
            var result = await Build<CustomerConsent>().SelectTo<GetCustomerConsentListDto>().ToListAsync();

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
                                         .ByConsentAccountId(_params.AccountId)
                                         .ByConsentStatus(_params.Status)
                                         .ByUserId(_params.UserId)
                                         .OrderByDescending(o => o.RequestedVisitTime)
                                         ;

            return query;
        }
    }

    public class GetCustomerConsentListParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid AccountId")]
        public string? AccountId { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
    }
}
