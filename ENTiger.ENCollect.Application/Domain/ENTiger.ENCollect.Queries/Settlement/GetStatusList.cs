using ENTiger.ENCollect.SettlementModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetStatusList : FlexiQueryEnumerableBridgeAsync<Settlement, GetStatusListDto>
    {
        protected readonly ILogger<GetStatusList> _logger;
        protected GetStatusListParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetStatusList(ILogger<GetStatusList> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetStatusList AssignParameters(GetStatusListParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetStatusListDto>> Fetch()
        {
            //return await Build<DispositionGroupMaster>().SelectTo<GetGroupMastersDto>().ToListAsync();
            var statusEnumList = SettlementStatusEnum.GetAll();
            List<GetStatusListDto> result = new List<GetStatusListDto>();
            foreach (var item in statusEnumList)
            {
                var status =  new GetStatusListDto
                {
                    Id = item.Value,
                    Name = item.DisplayName
                };
                result.Add(status);
            }

            return result;
        }

        protected override IQueryable<T1> Build<T1>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

    }

    /// <summary>
    ///
    /// </summary>
    public class GetStatusListParams : DtoBridge
    {
       
    }
}