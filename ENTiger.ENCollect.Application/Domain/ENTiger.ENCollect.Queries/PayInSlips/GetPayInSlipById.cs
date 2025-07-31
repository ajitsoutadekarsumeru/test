using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetPayInSlipById : FlexiQueryBridgeAsync<PayInSlip, GetPayInSlipByIdDto>
    {
        protected readonly ILogger<GetPayInSlipById> _logger;
        protected GetPayInSlipByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetPayInSlipById(ILogger<GetPayInSlipById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetPayInSlipById AssignParameters(GetPayInSlipByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetPayInSlipByIdDto> Fetch()
        {
            var payInSlip = Build<PayInSlip>();

            var result =await payInSlip.SelectTo<GetPayInSlipByIdDto>().FirstOrDefaultAsync();

            var slip = await payInSlip.FirstOrDefaultAsync();
            ICollection<PayInSlipBatchCollectionDto> model = new List<PayInSlipBatchCollectionDto>();
            if (slip.CollectionBatches != null && slip.CollectionBatches.Count() > 0)
            {
                foreach (var batch in slip.CollectionBatches)
                {
                    if (batch.Collections != null && batch.Collections.Count() > 0)
                    {
                        foreach (var collection in batch.Collections)
                        {
                            if (collection != null)
                            {
                                PayInSlipBatchCollectionDto obj = new PayInSlipBatchCollectionDto();
                                obj.ReceiptNo = collection.CustomId;
                                obj.Amount = collection.Amount;
                                if (collection.Cheque != null)
                                {
                                    obj.BankName = collection.Cheque.BankName;
                                    obj.BranchName = collection.Cheque.BranchName;
                                    obj.InstrumentNo = collection.Cheque.InstrumentNo;
                                    obj.MICR = collection.Cheque.MICRCode;
                                    obj.InstrumentDate = collection.Cheque.InstrumentDate;
                                    obj.IfscCode = collection.Cheque.IFSCCode;
                                }
                                model.Add(obj);
                            }
                        }
                    }
                }
                result.PayInSlipCollectionList = model;
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ById(_params.Id);
            return query;
        }
    }

    public class GetPayInSlipByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}