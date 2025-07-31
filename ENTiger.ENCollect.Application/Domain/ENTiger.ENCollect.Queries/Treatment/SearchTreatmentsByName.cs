using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchTreatmentsByName : FlexiQueryEnumerableBridgeAsync<Treatment, SearchTreatmentsByNameDto>
    {
        protected readonly ILogger<SearchTreatmentsByName> _logger;
        protected SearchTreatmentsByNameParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchTreatmentsByName(ILogger<SearchTreatmentsByName> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchTreatmentsByName AssignParameters(SearchTreatmentsByNameParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchTreatmentsByNameDto>> Fetch()
        {
            var result = await Build<Treatment>().SelectTo<SearchTreatmentsByNameDto>().ToListAsync();

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
                                    .ByTreatmentStartsWithName(_params.Name)
                                    .Where(a => a.IsDeleted == false)
                                    .OrderBy(a => a.Name).Take(10);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchTreatmentsByNameParams : DtoBridge
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {3} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}