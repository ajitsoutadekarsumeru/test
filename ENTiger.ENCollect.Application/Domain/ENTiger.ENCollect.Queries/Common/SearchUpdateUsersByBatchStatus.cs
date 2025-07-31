using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchUpdateUsersByBatchStatus : FlexiQueryEnumerableBridgeAsync<UsersUpdateFile, SearchUpdateUsersByBatchStatusDto>
    {
        protected readonly ILogger<SearchUpdateUsersByBatchStatus> _logger;
        protected SearchUpdateUsersByBatchStatusParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;

        ///// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchUpdateUsersByBatchStatus(ILogger<SearchUpdateUsersByBatchStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchUpdateUsersByBatchStatus AssignParameters(SearchUpdateUsersByBatchStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchUpdateUsersByBatchStatusDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;
            var result = await Build<UsersUpdateFile>().SelectTo<SearchUpdateUsersByBatchStatusDto>().ToListAsync();

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
                        .ByUsersUpdateFileUser(userId)
                        .ByUsersUpdateTransactionId(_params.TransactionId)
                        .ByUsersUpdateUploadedDate(_params.FileuploadDate)
                        .ByUsersUpdateFileStatus(_params.Status)
                        .ByUsersUpdateFileName(_params.FileName)
                        .OrderByDescending(a => a.CreatedDate);
            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchUpdateUsersByBatchStatusParams : DtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileuploadDate { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}