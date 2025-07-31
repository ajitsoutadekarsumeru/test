using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SecondaryAllocationFileUploadStatus : FlexiQueryPagedListBridgeAsync<SecondaryAllocationFile, SecondaryAllocationFileUploadStatusParams, SecondaryAllocationFileUploadStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SecondaryAllocationFileUploadStatus> _logger;
        protected SecondaryAllocationFileUploadStatusParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string TransactionId = string.Empty;
        private string status = string.Empty;
        private string FileName = string.Empty;
        private DateTime? FileuploadDate;
        private string AllocationMethod = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SecondaryAllocationFileUploadStatus(ILogger<SecondaryAllocationFileUploadStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SecondaryAllocationFileUploadStatus AssignParameters(SecondaryAllocationFileUploadStatusParams @params)
        {
            _params = @params;
            TransactionId = _params.TransactionId;
            status = _params.status;
            FileName = _params.FileName;
            FileuploadDate = _params.FileuploadDate;
            AllocationMethod = _params.AllocationMethod;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SecondaryAllocationFileUploadStatusDto>> Fetch()
        {
            var projection = await Build<SecondaryAllocationFile>().SelectTo<SecondaryAllocationFileUploadStatusDto>().ToListAsync();

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
                                    .ByTransactionId(TransactionId)
                                    .ByFileName(FileName)
                                    .ByUploadedDate(FileuploadDate)
                                    .ByAllocationMethod(AllocationMethod)
                                    .ByFileUploadedStatus(status)
                                    .OrderByDescending(b => b.CreatedDate);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SecondaryAllocationFileUploadStatusParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileuploadDate { get; set; }
        public int Take { get; set; }
        public string? AllocationMethod { get; set; }
    }
}