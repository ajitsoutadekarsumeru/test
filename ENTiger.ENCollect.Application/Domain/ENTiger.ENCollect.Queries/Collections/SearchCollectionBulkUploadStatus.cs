using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchCollectionBulkUploadStatus : FlexiQueryPagedListBridgeAsync<CollectionUploadFile, SearchCollectionBulkUploadStatusParams, SearchCollectionBulkUploadStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchCollectionBulkUploadStatus> _logger;
        protected SearchCollectionBulkUploadStatusParams _params;
        protected readonly IRepoFactory _repoFactory;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCollectionBulkUploadStatus(ILogger<SearchCollectionBulkUploadStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCollectionBulkUploadStatus AssignParameters(SearchCollectionBulkUploadStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchCollectionBulkUploadStatusDto>> Fetch()
        {
            var projection = await Build<CollectionUploadFile>().SelectTo<SearchCollectionBulkUploadStatusDto>().ToListAsync();

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
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                           .ByCustomId(_params.TransactionId)
                           .ByFileName(_params.FileName)
                           .ByUploadedDate(_params.FileuploadDate)
                           .ByFileUploadedStatus(_params.status);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchCollectionBulkUploadStatusParams : PagedQueryParamsDtoBridge
    {
        public string? TransactionId { get; set; }

        public string? status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileuploadDate { get; set; }

        public int skip { get; set; }
        public int take { get; set; }
    }
}
