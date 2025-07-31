using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetPayInSlipsForAck : FlexiQueryPagedListBridgeAsync<PayInSlip, GetPayInSlipsForAckParams, GetPayInSlipsForAckDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetPayInSlipsForAck> _logger;
        protected GetPayInSlipsForAckParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetPayInSlipsForAck(ILogger<GetPayInSlipsForAck> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetPayInSlipsForAck AssignParameters(GetPayInSlipsForAckParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetPayInSlipsForAckDto>> Fetch()
        {
            var projection = await Build<PayInSlip>().SelectTo<GetPayInSlipsForAckDto>().ToListAsync();

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
                                    .IncludePayInSlipUserWorkflow()
                                    .GetByCMSPayInSlipNo(_params.CMSPayInSlipNo)
                                    .GetByCMSPayInSlipCode(_params.ENCollectPayInSlipNumber)
                                    .DateRange(_params.DepositDateFrom, _params.DepositDateTo)
                                    .ByPayInSlipMode(_params.PaymentMode)
                                    .ByPayInSlipTypeMode(_params.PayinSlipType)
                                    .ByPayInSlipProductGroup(_params.ProductGroup)
                                    .ByAmount(_params.Amount)
                                    .ByPayInSlipStatus()
                                    .OrderByDescending(x => x.CreatedDate);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetPayInSlipsForAckParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid CMSPayInSlipNo")]
        public string? CMSPayInSlipNo { get; set; }

        public decimal? Amount { get; set; }
        public string? PaymentMode { get; set; }
        public DateTime? DepositDateFrom { get; set; }
        public DateTime? DepositDateTo { get; set; }
        public string? PayinSlipType { get; set; }
        public string? ENCollectPayInSlipNumber { get; set; }
        public string? ProductGroup { get; set; }
        public int Take { get; set; }
    }
}