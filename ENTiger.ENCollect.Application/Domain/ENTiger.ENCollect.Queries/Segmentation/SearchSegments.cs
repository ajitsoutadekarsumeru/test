using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchSegments : FlexiQueryPagedListBridgeAsync<Segmentation, SearchSegmentsParams, SearchSegmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchSegments> _logger;
        protected SearchSegmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        string ProductGroupName = string.Empty;
        string ProductName = string.Empty;
        string SubProductName = string.Empty;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchSegments(ILogger<SearchSegments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchSegments AssignParameters(SearchSegmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchSegmentsDto>> Fetch()
        {
            _repoFactory.Init(_params);

            List<string> Ids = new List<string>();
            Ids.Add(_params.ProductGroupId);
            Ids.Add(_params.ProductId);
            Ids.Add(_params.SubProductId);

            var categoryItems = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => Ids.Contains(a.Id)).ToListAsync();
            if (categoryItems.Count() > 0)
            {
                var prodgroupres = categoryItems.Where(a => a.Id == _params.ProductGroupId).FirstOrDefault();
                ProductGroupName = prodgroupres != null ? prodgroupres.Name : "";

                var prodres = categoryItems.Where(a => a.Id == _params.ProductId).FirstOrDefault();
                ProductName = prodres != null ? prodres.Name : "";

                var subres = categoryItems.Where(a => a.Id == _params.SubProductId).FirstOrDefault();
                SubProductName = subres != null ? subres.Name : "";
            }

            var projection = await Build<Segmentation>().SelectTo<SearchSegmentsDto>().ToListAsync();

            var result = BuildPagedOutput(projection);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {   

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .BySegmentCreatedBy(_params.CreatedBy)
                                    .BySegmentCreatedDate(_params.CreatedDate)
                                    .BySegmentName(_params.SegmentName)
                                    .BySegmentProductGroup(ProductGroupName)
                                    .BySegmentProduct(ProductName)
                                    .BySegmentSubProduct(SubProductName)
                                    .BySegmentBOM_Bucket(_params.BOM_Bucket)
                                    .BySegmentCurrentBucket(_params.CurrentBucket)
                                    .BySegmentNPAFlag(_params.NPA_Flag)
                                    .BySegmentZone(_params.Zone)
                                    .BySegmentState(_params.State)
                                    .BySegmentCity(_params.City)
                                    .BySegmentBranch(_params.Branch)
                                    .ByIsDeleted()
                                    .OrderByDescending(x => x.LastModifiedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchSegmentsParams : PagedQueryParamsDtoBridge
    {
        public string? SegmentName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ProductGroupId { get; set; }

        public string? ProductId { get; set; }

        public string? SubProductId { get; set; }

        public string? BOM_Bucket { get; set; }

        public string? CurrentBucket { get; set; }

        public string? NPA_Flag { get; set; }

        public string? Zone { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Branch { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}