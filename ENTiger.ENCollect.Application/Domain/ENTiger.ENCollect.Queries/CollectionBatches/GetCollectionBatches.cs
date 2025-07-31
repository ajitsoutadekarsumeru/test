using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollectionBatches : FlexiQueryBridgeAsync<CollectionBatch, GetCollectionBatchDto>
    {
        protected readonly ILogger<GetCollectionBatches> _logger;
        protected GetCollectionBatchParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CollectionBatchWorkflowState state;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollectionBatches(ILogger<GetCollectionBatches> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCollectionBatches AssignParameters(GetCollectionBatchParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetCollectionBatchDto> Fetch()
        {
            var result = await Build<CollectionBatch>().Select(p => new GetCollectionBatchDto
            {
                CollectionBatch = new CollectionBatchGetBatchByIdDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    ModeOfPayment = p.ModeOfPayment,
                    BatchCode = p.CustomId,
                    BatchType = p.BatchType,
                    ProductGroup = p.ProductGroup,
                    AcknowledgedBy = p.AcknowledgedById,
                    Agency = p.CollectionBatchOrgId,
                    CreatedDate = p.CreatedDate,
                    status = p.CollectionBatchWorkflowState.Name
                },
                CollectionDetails = p.CollectionBatchWorkflowState.Name == "Dissolved"
                                ? null
                                : p.Collections
                                    .Where(a => a.CollectionWorkflowState.Name != "ReadyForBatch")
                                    .Select(c => new CollectionDetailsGetBatchByIdDto
                                    {
                                        Id = c.Id,
                                        ReceiptNo = c.CustomId,
                                        collectorFistName = c.Collector.FirstName,
                                        collectorMiddleName = c.Collector.MiddleName,
                                        collectorLastName = c.Collector.LastName,
                                        collectorId = c.CollectorId,
                                        CustomerName = c.CustomerName,
                                        TransactionNumber = c.TransactionNumber,
                                        AccountNo = c.Account.CustomId,
                                        product = c.Account.PRODUCT,
                                        PaymentMode = c.CollectionMode,
                                        receiptDate = c.CollectionDate,
                                        receivedAtAgency = c.AcknowledgedDate,
                                        BatchId = c.CollectionBatchId,
                                        Amount = c.Amount,
                                        Status = c.CollectionWorkflowState.Name,
                                        InstrumentNo = c.Cheque.InstrumentNo,
                                        InstrumentDate = c.Cheque.InstrumentDate,
                                        DraweeBank = c.Cheque.BankName,
                                        DraweeBranch = c.Cheque.BranchName,
                                        OverdueAmount = c.yOverdueAmount,
                                        ForeclosureAmount = c.yForeClosureAmount,
                                        BounceCharges = c.yBounceCharges,
                                        PenalAmount = c.yPenalInterest,
                                        Settlement = c.Settlement,
                                        OtherCharges = c.othercharges
                                    }).ToList()
            }).SingleOrDefaultAsync();

            if (result != null)
            {
                if (!String.IsNullOrEmpty(result.CollectionBatch.AcknowledgedBy))
                {
                    string AckAgentorgid = await _repoFactory.GetRepo().Find<CompanyUser>(result.CollectionBatch.AcknowledgedBy)
                        .Select(a => a.BaseBranchId)
                        .FirstOrDefaultAsync();

                    var ackagentOrgName = await _repoFactory.GetRepo().Find<ApplicationOrg>(AckAgentorgid)
                         .Select(a => new
                         {
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                         }).SingleOrDefaultAsync();

                    if (ackagentOrgName != null)
                    {
                        result.CollectionBatch.BranchAcknowleged = ackagentOrgName.FirstName + "" + ackagentOrgName.LastName;
                    }

                    result.CollectionBatch.AcknowledgedBy = await _repoFactory.GetRepo().Find<CompanyUser>(result.CollectionBatch.AcknowledgedBy)
                            .Select(a => a.FirstName + " " + a.LastName)
                            .SingleOrDefaultAsync();
                }

                if (!String.IsNullOrEmpty(result.CollectionBatch.Agency))
                {
                    var CollectionBatchOrgName = await _repoFactory.GetRepo().Find<ApplicationOrg>(result.CollectionBatch.Agency)
                        .Select(a => new
                        {
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                        }).SingleOrDefaultAsync();

                    if (CollectionBatchOrgName != null)
                    {
                        result.CollectionBatch.Agency = CollectionBatchOrgName.FirstName + "" + CollectionBatchOrgName.LastName;
                    }
                }
            }

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
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string LoggedUserId = _flexAppContext.UserId;
            state = WorkflowStateFactory.GetCollectionBatchWorkflowState(_params.BatchStatus);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                   .ByCreatedUser(LoggedUserId)
                                   .ByBatchCustomId(_params.BatchId)
                                   .ByCollectionBatchdWorkFLowState(state)
                                   .GetByDateRange(_params.BatchCreationDateFrom, _params.BatchCreationDateTo); ;

            //Build Your Query Here

            return query;
        }

        public static CollectionBatchWorkflowState GetCollectionBatchWorkflowState(string status)
        {
            CollectionBatchWorkflowState result = null;
            switch (status)
            {
                case "Payment Batch Created":
                    result = new CollectionBatchCreated();
                    break;

                case "Payment Batch Acknowledged By Branch":
                    result = new CollectionBatchAcknowledged();
                    break;

                case "Payment Batch Dissolved":
                    result = new Dissolved();
                    break;
            }

            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCollectionBatchParams : DtoBridge
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid BatchId")]
        public string? BatchId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid BatchStatus")]
        public string? BatchStatus { get; set; }

        public DateTime? BatchCreationDateFrom { get; set; }
        public DateTime? BatchCreationDateTo { get; set; }
        public string? LoggedUserId { get; set; }
    }
}