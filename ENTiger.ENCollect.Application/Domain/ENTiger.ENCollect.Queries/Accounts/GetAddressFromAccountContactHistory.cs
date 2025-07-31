using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Linq;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAddressFromAccountContactHistory : FlexiQueryEnumerableBridge<AccountContactHistory, GetAddressFromAccountContactHistoryDto>
    {
        
        protected readonly ILogger<GetAddressFromAccountContactHistory> _logger;
        protected GetAddressFromAccountContactHistoryParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAddressFromAccountContactHistory(ILogger<GetAddressFromAccountContactHistory> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAddressFromAccountContactHistory AssignParameters(GetAddressFromAccountContactHistoryParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GetAddressFromAccountContactHistoryDto> Fetch()
        {
            var result = Build<AccountContactHistory>().SelectTo<GetAddressFromAccountContactHistoryDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByAddressAndAccountId(_params.AccountId).OrderByDescending(x=>x.LastModifiedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetAddressFromAccountContactHistoryParams : DtoBridge
    {
        public string AccountId { get; set; }
    }
}
