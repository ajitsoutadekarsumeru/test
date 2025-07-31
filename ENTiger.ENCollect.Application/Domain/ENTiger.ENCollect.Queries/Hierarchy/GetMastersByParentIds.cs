using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMastersByParentIds : FlexiQueryEnumerableBridgeAsync<HierarchyMaster, GetMastersByParentIdsDto>
    {
        
        protected readonly ILogger<GetMastersByParentIds> _logger;
        protected GetMastersByParentIdsParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMastersByParentIds(ILogger<GetMastersByParentIds> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMastersByParentIds AssignParameters(GetMastersByParentIdsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMastersByParentIdsDto>> Fetch()
        {
            var result = await Build<HierarchyMaster>().SelectTo<GetMastersByParentIdsDto>().ToListAsync();

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
                                    .Where(w => w.ParentId != null && _params.Ids.Contains(w.ParentId));
            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMastersByParentIdsParams : DtoBridge
    {
        [Required]
        public List<string> Ids { get; set; }
    }
}
