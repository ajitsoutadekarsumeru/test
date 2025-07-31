using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class PrimaryUnAllocationBatchStatus : FlexiQueryPagedListBridgeAsync<PrimaryUnAllocationFile, PrimaryUnAllocationBatchStatusParams, PrimaryUnAllocationBatchStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<PrimaryUnAllocationBatchStatus> _logger;
        protected PrimaryUnAllocationBatchStatusParams _params;
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
        public PrimaryUnAllocationBatchStatus(ILogger<PrimaryUnAllocationBatchStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual PrimaryUnAllocationBatchStatus AssignParameters(PrimaryUnAllocationBatchStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<PrimaryUnAllocationBatchStatusDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;

            var projection = await Build<PrimaryUnAllocationFile>().SelectTo<PrimaryUnAllocationBatchStatusDto>().ToListAsync();

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
                                    .ByPrimaryUnAllocationFileUser(userId)
                                    .ByPrimaryTransactionId(_params.TransactionId)
                                    .ByPrimaryUploadedDate(_params.FileuploadDate)
                                    .ByPrimaryFileStatus(_params.status)
                                    .ByPrimaryFileName(_params.FileName)
                                    .ByPrimaryUnAllocationType(_params.UnAllocationType)
                                    .OrderByDescending(x => x.CreatedDate); // Corrected the order by clause

            //Build Your Query With All Parameters Here
            _params.Take = _params.Take == 0 ? 50 : _params.Take;
            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class PrimaryUnAllocationBatchStatusParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid TransactionId")]
        public string? TransactionId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string? status { get; set; }

        public string? FileName { get; set; }
        public string? UnAllocationType { get; set; }

        public DateTime? FileuploadDate { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}