using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SecondaryUnAllocationBatchStatus : FlexiQueryPagedListBridgeAsync<SecondaryUnAllocationFile, SecondaryUnAllocationBatchStatusParams, SecondaryUnAllocationBatchStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SecondaryUnAllocationBatchStatus> _logger;
        protected SecondaryUnAllocationBatchStatusParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId, tenantId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SecondaryUnAllocationBatchStatus(ILogger<SecondaryUnAllocationBatchStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SecondaryUnAllocationBatchStatus AssignParameters(SecondaryUnAllocationBatchStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SecondaryUnAllocationBatchStatusDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;
            var projection = await Build<SecondaryUnAllocationFile>().SelectTo<SecondaryUnAllocationBatchStatusDto>().ToListAsync();

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
                        .BySecondaryUnAllocationFileUser(userId)
                        .BySecondaryTransactionId(_params.TransactionId)
                        .BySecondaryUploadedDate(_params.FileuploadDate)
                        .BySecondaryFileStatus(_params.status)
                        .BySecondaryFileName(_params.FileName)
                        .BySecondaryUnAllocationType(_params.UnAllocationType)
                        .OrderByDescending(x => x.CreatedDate);

            //Build Your Query With All Parameters Here
            _params.Take = _params.Take == 0 ? 50 : _params.Take;
            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SecondaryUnAllocationBatchStatusParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? status { get; set; }

        public string? FileName { get; set; }
        public string? UnAllocationType { get; set; }

        public DateTime? FileuploadDate { get; set; }

        public int? Skip { get; set; }

        public int Take { get; set; }
    }
}