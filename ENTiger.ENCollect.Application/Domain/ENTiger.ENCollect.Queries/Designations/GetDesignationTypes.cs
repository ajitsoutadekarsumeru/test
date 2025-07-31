using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDesignationTypes : FlexiQueryEnumerableBridgeAsync<DesignationType, GetDesignationTypesDto>
    {
        protected readonly ILogger<GetDesignationTypes> _logger;
        protected GetDesignationTypesParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDesignationTypes(ILogger<GetDesignationTypes> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetDesignationTypes AssignParameters(GetDesignationTypesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetDesignationTypesDto>> Fetch()
        {
            return await Build<DesignationType>().SelectTo<GetDesignationTypesDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByDeleteDesignationType();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetDesignationTypesParams : DtoBridge
    {
    }
}