using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Linq;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetRoleBasedSearchConfig : FlexiQueryEnumerableBridgeAsync<AccountScopeConfiguration, GetRoleBasedSearchConfigDto>
    {
        
        protected readonly ILogger<GetRoleBasedSearchConfig> _logger;
        protected GetRoleBasedSearchConfigParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetRoleBasedSearchConfig(ILogger<GetRoleBasedSearchConfig> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetRoleBasedSearchConfig AssignParameters(GetRoleBasedSearchConfigParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetRoleBasedSearchConfigDto>> Fetch()
        {
            var result = await Build<AccountScopeConfiguration>().SelectTo<GetRoleBasedSearchConfigDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetRoleBasedSearchConfigParams : DtoBridge
    {

    }
}
