using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMastersById : FlexiQueryEnumerableBridgeAsync<HierarchyMaster, GetMastersByIdDto>
    {
        
        protected readonly ILogger<GetMastersById> _logger;
        protected GetMastersByIdParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMastersById(ILogger<GetMastersById> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMastersById AssignParameters(GetMastersByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMastersByIdDto>> Fetch()
        {
            var result = await Build<HierarchyMaster>().SelectTo<GetMastersByIdDto>().ToListAsync();

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
                                        .Where(w => w.LevelId == _params.Id);
            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMastersByIdParams : DtoBridge
    {
        [Required]
        public string Id { get; set; }
    }
}
