using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDuePDCDetails : FlexiQueryEnumerableBridgeAsync<Collection, GetDuePDCDetailsDto>
    {
        protected readonly ILogger<GetDuePDCDetails> _logger;
        protected GetDuePDCDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private ApplicationUser party = new ApplicationUser();
        private string Id;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDuePDCDetails(ILogger<GetDuePDCDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetDuePDCDetails AssignParameters(GetDuePDCDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetDuePDCDetailsDto>> Fetch()
        {
            Id = party.Id; //ApplicationUser Id
            var result = await Build<Collection>().SelectTo<GetDuePDCDetailsDto>().ToListAsync();

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
                .ByCollector(Id).ByCheque()
            .ByInstrumentDateRange(_params.FromDate, _params.ToDate); ;

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetDuePDCDetailsParams : DtoBridge
    {
        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }
    }
}