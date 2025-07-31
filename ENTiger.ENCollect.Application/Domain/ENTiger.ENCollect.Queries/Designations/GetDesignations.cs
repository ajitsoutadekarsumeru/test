using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDesignations : FlexiQueryEnumerableBridgeAsync<Designation, GetDesignationsDto>
    {
        protected readonly ILogger<GetDesignations> _logger;
        protected GetDesignationsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDesignations(ILogger<GetDesignations> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetDesignations AssignParameters(GetDesignationsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetDesignationsDto>> Fetch()
        {
            return await Build<Designation>().SelectTo<GetDesignationsDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByDeleteDesignation();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetDesignationsParams : DtoBridge
    {
    }
}