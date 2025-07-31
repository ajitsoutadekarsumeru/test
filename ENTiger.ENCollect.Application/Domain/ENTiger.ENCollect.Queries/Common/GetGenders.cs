using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetGenders : FlexiQueryEnumerableBridgeAsync<CategoryItem, GetGendersDto>
    {
        protected readonly ILogger<GetGenders> _logger;
        protected GetGendersParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string? genderCategoryMasterID;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetGenders(ILogger<GetGenders> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetGenders AssignParameters(GetGendersParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetGendersDto>> Fetch()
        {
            _repoFactory.Init(_params);

            genderCategoryMasterID = await _repoFactory.GetRepo().FindAll<CategoryMaster>()
                              .Where(a => string.Equals(a.Name, "gender"))
                              .Select(s => s.Id)
                              .FirstOrDefaultAsync();

            var result = await Build<CategoryItem>().SelectTo<GetGendersDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.CategoryMasterId == genderCategoryMasterID);
            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetGendersParams : DtoBridge
    {
    }
}