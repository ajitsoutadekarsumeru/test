using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetOnlinePaymentReport : FlexiQueryEnumerableBridgeAsync<PaymentTransaction, GetOnlinePaymentReportDto>
    {
        protected readonly ILogger<GetOnlinePaymentReport> _logger;
        protected GetOnlinePaymentReportParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetOnlinePaymentReport(ILogger<GetOnlinePaymentReport> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetOnlinePaymentReport AssignParameters(GetOnlinePaymentReportParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetOnlinePaymentReportDto>> Fetch()
        {
            List<GetOnlinePaymentReportDto> result = new List<GetOnlinePaymentReportDto>();
            //result = await Build<PaymentTransaction>().SelectTo<GetOnlinePaymentReportDto>().ToListAsync();

            // Execute the query and set output
            return result;

            #region code
            // Parse dates
            DateTime? startDate = string.IsNullOrEmpty(_params.StartDate)
                ? (DateTime?)null
                    : DateTime.ParseExact(_params.StartDate, "dd-MM-yyyy", null);

            DateTime? endDate = string.IsNullOrEmpty(_params.EndDate)
            ? (DateTime?)null
                : DateTime.ParseExact(_params.EndDate, "dd-MM-yyyy", null);

            string status = _params.Status;
            string userId = Convert.ToString(_params.UserId);

            var query = _repoFactory.GetRepo()
.FindAll<PaymentTransaction>()
.Where(pt =>
(_params.StartDate == null || pt.TransactionDate >= startDate) &&
(_params.EndDate == null || pt.TransactionDate <= endDate) &&
(_params.Status == null || pt.Status == _params.Status) &&
(_params.UserId == null || pt.CreatedBy == _params.UserId))
.Select(pt => new
{
PaymentTransactionId = pt.Id,
pt.MerchantReferenceNumber,
pt.MerchantTransactionId,
pt.BankTransactionId,
pt.BankReferenceNumber,
pt.Amount,
pt.Currency,
pt.TransactionDate,
pt.StatusCode,
PaymentStatus = pt.Status,
pt.ResponseMessage,
pt.ErrorMessage,
pt.ErrorCode,
pt.IsPaid,
pt.TransactionStatus,
pt.RRN,
pt.AuthCode,
pt.CardNumber,
pt.CardType,
pt.CardHolderName,
LoanAccountNumber = pt.LoanAccount.CustomId,
CustomerName = pt.LoanAccount.CUSTOMERNAME,
ProductName = pt.LoanAccount.PRODUCT,
pt.LoanAccount.CURRENT_BUCKET,
AgencyId = pt.LoanAccount.Agency.Id,
AgencyName = pt.LoanAccount.Agency.FirstName,
    //  AgentId = pt.CreatedByNavigation.Id,
    // AgentName = pt.CreatedByNavigation.FullName,
    // AgentEmail = pt.CreatedByNavigation.Email,
    //  ReceiptNumber = pt.LoanAccount.Collections.FirstOrDefaultAsync().Receipt.CustomId,
    //   ReceiptDate = pt.LoanAccount.Collections.FirstOrDefaultAsync().CollectionDate,
    //  TotalReceiptAmount = pt.LoanAccount.Collections.FirstOrDefaultAsync().Amount
})
.OrderByDescending(pt => pt.TransactionDate)
.ToListAsync();

            #endregion code
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetOnlinePaymentReportParams : DtoBridge
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string? UserId { get; set; }
    }
}