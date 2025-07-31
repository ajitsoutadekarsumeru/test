using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchTreatments : FlexiQueryBridgeAsync<Treatment, SearchTreatmentsDto>
    {
        protected readonly ILogger<SearchTreatments> _logger;
        protected SearchTreatmentsParams _params;
        protected readonly IRepoFactory _repoFactory;
        public int _take;
        public int _skip;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchTreatments(ILogger<SearchTreatments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchTreatments AssignParameters(SearchTreatmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<SearchTreatmentsDto> Fetch()
        {
            if (_params.Take < 0)
            {
                _take = 0;
            }
            if (_params.Take == 0)
            {
                _take = 50;
            }
            else
            {
                _take = _params.Take;
            }
            if (_params.Skip < 0)
            {
                _skip = 0;
            }
            else
            {
                _skip = _params.Skip;
            }

            SearchTreatmentsDto output = new SearchTreatmentsDto();

            output.count =  Build<Treatment>().Count();

            var model = await Build<Treatment>().SelectTo<SearchTreatmentOutputDto>().Skip(_skip).Take(_take).ToListAsync();

            List<string> createdbyIds = model.Select(a => a.CreatedBy).ToList();

            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => createdbyIds.Contains(a.Id)).ToListAsync();
            model.ForEach(a =>
            {
                a.CreatedBy = users.Where(x => x.Id == a.CreatedBy).Select(b => b.FirstName + " " + b.LastName).FirstOrDefault();
            });

            List<string> treatmentids = model.Select(a => a.Id).ToList();

            var executionhistory = await _repoFactory.GetRepo().FindAll<TreatmentHistory>().Where(a => treatmentids.Contains(a.TreatmentId)).ToListAsync();
            model.ForEach(a =>
            {
                a.ExecutionHistory = executionhistory.Where(b => b.TreatmentId == a.Id).ToList().Count().ToString();
            });

            output.outputmodel = model;
            return output;
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
                                    .ByTreatmentName(_params.Name)
                                    .ByTreatmentCreatedDate(_params.CreatedOn)
                                    .ByTreatmentCreatedBy(_params.CreatedBy)
                                    .ByTreatmentIsDeleted()
                                    .Where(a => a.IsDeleted == false).OrderByDescending(a => a.LastModifiedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchTreatmentsParams : DtoBridge
    {
        [StringLength(50)]
        public string? Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}