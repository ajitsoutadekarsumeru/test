using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollectionBatches : FlexiQueryEnumerableBridgeAsync<CollectionBatch, GetCollectionBatchesDto>
    {
        protected readonly ILogger<GetCollectionBatches> _logger;
        protected GetCollectionBatchesParams _params;
        protected readonly IRepoFactory _repoFactory;
        private bool includeCollection;
        private string[] modeofpayment = new[] { "cheque", "cheque/dd", "dd" };

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
        public virtual GetCollectionBatches AssignParameters(GetCollectionBatchesParams @params)
        {
            _params = @params;
            includeCollection = _params.PaymentMode != null && modeofpayment.Contains(_params.PaymentMode, StringComparer.OrdinalIgnoreCase);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetCollectionBatchesDto>> Fetch()
        {
            ICollection<GetCollectionBatchesDto> result = new List<GetCollectionBatchesDto>();
            var batches = Build<CollectionBatch>();

            List<Collection> collections = new List<Collection>();
            if (includeCollection)
            {
                collections = await _repoFactory.GetRepo().FindAll<Collection>().FlexInclude(a => a.Cheque).FlexInclude(a => a.Collector)
                                    .Where(a => _params.BatchIds.Contains(a.CollectionBatchId)).ToListAsync();
            }

            foreach (var batch in batches)
            {
                var model = new GetCollectionBatchesDto
                {
                    collectionBatch = FlexOpus.Convert<CollectionBatch, PayInSlipCollectionBatchDto>(batch),
                    collectionDetails = new List<PayInSlipCollectionDetailsDto>()
                };

                if (includeCollection)
                {
                    var batchCollections = collections.Where(i => i.CollectionBatchId == batch.Id).ToList();
                    if (batchCollections.Any())
                    {
                        foreach (var collection in batchCollections)
                        {
                            //var collectionDto = FlexOpus.Convert<Collection, PayInSlipCollectionDetailsDto>(collection);
                            var collectionDto = new PayInSlipCollectionDetailsDto()
                            {
                                Amount = (collection.Amount ?? 0).ToString(),
                                PaymentDate = collection.CollectionDate.ToString(),
                                ReceiptNo = collection.CustomId,
                                PaymentMode = collection.CollectionMode,
                                CollecterCode = collection.Collector?.CustomId,
                                CollecterName = collection.Collector?.FirstName
                            };
                            // Initialize Denominationdetails directly with the converted value
                            if (collection.Cheque != null)
                            {
                                //collectionDto.Denominationdetails = FlexOpus.Convert<Cheque, ChequeDetailsDto>(collection.Cheque);
                                collectionDto.Denominationdetails = new ChequeDetailsDto()
                                {
                                    Id = collection.Cheque.Id,
                                    BankName = collection.Cheque.BankName,
                                    BranchName = collection.Cheque.BranchName,
                                    InstrumentNo = collection.Cheque.InstrumentNo,
                                    InstrumentDate = collection.Cheque.InstrumentDate != null ? collection.Cheque.InstrumentDate.ToString() : "",
                                    MICRCode = collection.Cheque.MICRCode,
                                    IFSCCode = collection.Cheque.IFSCCode,
                                };
                            }
                            model.collectionDetails.Add(collectionDto);
                        }
                    }
                }
                result.Add(model);
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByIds(_params.BatchIds);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCollectionBatchesParams : DtoBridge
    {
        public List<string> BatchIds { get; set; }
        public string PaymentMode { get; set; }
    }
}