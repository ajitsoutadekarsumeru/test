using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.ReportsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCollectionIntensityDetails : FlexiQueryPagedListBridge<LoanAccount, GetCollectionIntensityDetailsParams, GetCollectionIntensityDetailsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetCollectionIntensityDetails> _logger;
        protected GetCollectionIntensityDetailsParams _params;
        protected readonly RepoFactory _repoFactory;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollectionIntensityDetails(ILogger<GetCollectionIntensityDetails> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCollectionIntensityDetails AssignParameters(GetCollectionIntensityDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override FlexiPagedList<GetCollectionIntensityDetailsDto> Fetch()
        {
            var projection = Build<LoanAccount>().SelectTo<GetCollectionIntensityDetailsDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.PageSize);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetCollectionIntensityDetailsParams : PagedQueryParamsDtoBridge
    {

    }
}
