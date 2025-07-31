using Microsoft.EntityFrameworkCore;
namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDepositSlipById : FlexiQueryBridgeAsync<PayInSlip, GetDepositSlipByIdDto>
    {
        protected readonly ILogger<GetDepositSlipById> _logger;
        protected GetDepositSlipByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDepositSlipById(ILogger<GetDepositSlipById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetDepositSlipById AssignParameters(GetDepositSlipByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetDepositSlipByIdDto> Fetch()
        {
            var payInSlip = Build<PayInSlip>();

            var result = await payInSlip.SelectTo<GetDepositSlipByIdDto>().FirstOrDefaultAsync();

            var slip = await payInSlip.FirstOrDefaultAsync();
            List<DepositSlipBatchCollectionDto> receiptDetails = new List<DepositSlipBatchCollectionDto>();
            result.ReceiptDetails = receiptDetails;
            if (slip != null && slip.CollectionBatches != null)
            {
                var collections = slip.CollectionBatches.Select(x => x.Collections).FirstOrDefault();
                if (collections != null)
                {
                    foreach (var collection in collections)
                    {
                        DepositSlipBatchCollectionDto receipt = new DepositSlipBatchCollectionDto();
                        receipt.Id = collection.ReceiptId;
                        receipt.Amount = collection?.Amount?.ToString();
                        receipt.ReceiptNo = collection?.CustomId;
                        receipt.ModeOfPayment = collection?.CollectionMode;
                        receipt.ReceiptDate = collection.CollectionDate.Value;
                        if (collection.Cheque != null)
                        {
                            receipt.DraweeBankName = collection?.Cheque?.BankName;
                            receipt.DraweeBranchName = collection?.Cheque?.BranchName;
                            receipt.InstrumentNo = collection?.Cheque?.InstrumentNo;
                            receipt.IFSC = collection?.Cheque?.IFSCCode;
                            receipt.MICR = collection?.Cheque?.MICRCode;
                        }
                        receiptDetails.Add(receipt);
                    }
                }
                if (receiptDetails.Count > 0)
                    result.ReceiptDetails = receiptDetails;
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id)
                                    .FlexInclude(x => x.CollectionBatches)
                                    .FlexInclude("CollectionBatches.Collections.Receipt")
                                    .FlexInclude("CollectionBatches.Collections.Cheque");
            return query;
        }
    }

    public class GetDepositSlipByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}