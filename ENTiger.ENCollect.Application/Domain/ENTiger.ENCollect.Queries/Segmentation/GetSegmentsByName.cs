using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSegmentsByName : FlexiQueryEnumerableBridgeAsync<Segmentation, GetSegmentsByNameDto>
    {
        protected readonly ILogger<GetSegmentsByName> _logger;
        protected GetSegmentsByNameParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSegmentsByName(ILogger<GetSegmentsByName> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetSegmentsByName AssignParameters(GetSegmentsByNameParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetSegmentsByNameDto>> Fetch()
        {
            return await Build<Segmentation>().SelectTo<GetSegmentsByNameDto>().ToListAsync();
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
                                 .Where(a => a.IsDeleted == false &&
                                  (a.IsDisabled == false || a.IsDisabled == null) &&
                                  a.Name.StartsWith(_params.name));


            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetSegmentsByNameParams : DtoBridge
    {
        public string? name { get; set; }
    }
}