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
    public class GetMobileFromAccountContactHistory : FlexiQueryEnumerableBridge<AccountContactHistory, GetMobileFromAccountContactHistoryDto>
    {

        protected readonly ILogger<GetMobileFromAccountContactHistory> _logger;
        protected GetMobileFromAccountContactHistoryParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMobileFromAccountContactHistory(ILogger<GetMobileFromAccountContactHistory> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMobileFromAccountContactHistory AssignParameters(GetMobileFromAccountContactHistoryParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GetMobileFromAccountContactHistoryDto> Fetch()
        {
            _repoFactory.Init(_params);

            var result = Build<AccountContactHistory>().SelectTo<GetMobileFromAccountContactHistoryDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByMobileAndAccountId(_params.AccountId).OrderByDescending(x => x.LastModifiedDate); 

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMobileFromAccountContactHistoryParams : DtoBridge
    {
        public string AccountId { get; set; }
    }
}
