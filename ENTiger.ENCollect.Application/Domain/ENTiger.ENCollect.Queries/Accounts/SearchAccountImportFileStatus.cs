using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAccountImportFileStatus : FlexiQueryEnumerableBridgeAsync<MasterFileStatus, SearchAccountImportFileStatusDto>
    {
        protected readonly ILogger<SearchAccountImportFileStatus> _logger;
        protected SearchAccountImportFileStatusParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchAccountImportFileStatus(ILogger<SearchAccountImportFileStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchAccountImportFileStatus AssignParameters(SearchAccountImportFileStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchAccountImportFileStatusDto>> Fetch()
        {
            var result = await Build<MasterFileStatus>().SelectTo<SearchAccountImportFileStatusDto>().ToListAsync();

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
                                    .ByMasterFileName(_params.FileName)
                                    .ByMasterFileUploadedDate(_params.FileuploadDate)
                                    .ByMasterFileStatus(_params.status)
                                    .ByMasterFileType("Account")
                                    .OrderByDescending(x => x.CreatedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAccountImportFileStatusParams : DtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileuploadDate { get; set; }
    }
}