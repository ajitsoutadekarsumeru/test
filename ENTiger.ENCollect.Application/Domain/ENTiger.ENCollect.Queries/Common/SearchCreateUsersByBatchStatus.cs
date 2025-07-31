using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCreateUsersByBatchStatus : FlexiQueryEnumerableBridgeAsync<UsersCreateFile, SearchCreateUsersByBatchStatusDto>
    {
        protected readonly ILogger<SearchCreateUsersByBatchStatus> _logger;
        protected SearchCreateUsersByBatchStatusParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCreateUsersByBatchStatus(ILogger<SearchCreateUsersByBatchStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCreateUsersByBatchStatus AssignParameters(SearchCreateUsersByBatchStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchCreateUsersByBatchStatusDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;

            var result = await Build<UsersCreateFile>().SelectTo<SearchCreateUsersByBatchStatusDto>().ToListAsync();

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
                                    .ByUsersCreateFileUser(userId)
                                    .ByUsersCreateTransactionId(_params.TransactionId)
                                    .ByUsersCreateUploadedDate(_params.FileUploadedDate)
                                    .ByUsersCreateFileStatus(_params.Status)
                                    .ByUsersCreateFileName(_params.FileName)
                                    .ByUsersCreateUserType(_params.UserType)
                                    .ByCreatedDate();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCreateUsersByBatchStatusParams : DtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileUploadedDate { get; set; }

        public string? UserType { get; set; }
    }
}