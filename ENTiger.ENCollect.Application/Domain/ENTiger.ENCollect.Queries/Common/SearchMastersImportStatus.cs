using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchMastersImportStatus : FlexiQueryEnumerableBridgeAsync<MasterFileStatus, SearchMastersImportStatusDto>
    {
        protected readonly ILogger<SearchMastersImportStatus> _logger;
        protected SearchMastersImportStatusParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchMastersImportStatus(ILogger<SearchMastersImportStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchMastersImportStatus AssignParameters(SearchMastersImportStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchMastersImportStatusDto>> Fetch()
        {
            var result = await Build<MasterFileStatus>().SelectTo<SearchMastersImportStatusDto>().ToListAsync();

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
                                    .ByMasterFileTransactionId(_params.TransactionId)
                                    .ByMasterFileType(_params.FileType)
                                    .ByMasterFileName(_params.FileName)
                                    .ByMasterFileUploadedDate(_params.UploadedDate)
                                    .ByMasterFileStatus(_params.Status)
                                    .OrderByDescending(x => x.CreatedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchMastersImportStatusParams : DtoBridge
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid FileType")]
        public string? FileType { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }

        public string? FileName { get; set; }

        public DateTime? UploadedDate { get; set; }
    }
}