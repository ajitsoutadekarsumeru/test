using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserNotification : FlexiQueryBridgeAsync<GetUserNotificationDto>
    {
        protected readonly ILogger<GetUserNotification> _logger;
        protected GetUserNotificationParams _params;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetUserNotification(ILogger<GetUserNotification> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUserNotification AssignParameters(GetUserNotificationParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Fetches user notifications for loan account status.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a <see cref="GetUserNotificationDto"/> result containing user notifications.</returns>
        public override async Task<GetUserNotificationDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext(); // do not remove this line
            _repoFactory.Init(_params);

            string userId = _flexAppContext.UserId;

            var result = new GetUserNotificationDto();
            await FetchLoanAccountNotificationDetails(result, userId);

            return result;
        }

        /// <summary>
        /// Fetches the loan account notification details for the specified user.
        /// </summary>
        /// <param name="notificationDto">The DTO to populate with notification details.</param>
        /// <param name="collectorUserId">The ID of the collector user to fetch the notifications for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task FetchLoanAccountNotificationDetails(GetUserNotificationDto notificationDto, string collectorUserId)
        {
            var currentDate = DateTime.Now;
            var startOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            var allocatedLoanAccounts = await GetAllocatedLoanAccountsAsync(collectorUserId, currentDate.Month, currentDate.Year);
            if (!allocatedLoanAccounts.Any())
            {
                SetDefaultMessages(notificationDto);
                return;
            }

            var allocatedAccountIds = allocatedLoanAccounts.Select(a => a.Id).ToList();
            var feedbackAccountIds = await GetFeedbackAccountIdsWithActivityAsync(allocatedAccountIds, startOfCurrentMonth);

            var unattemptedAccountIds = allocatedAccountIds.Except(feedbackAccountIds).ToList();

            notificationDto.TrailGapMessage = ComposeUnattemptedAccountsMessage(allocatedLoanAccounts, unattemptedAccountIds);
            notificationDto.TodaysPTPMessage = ComposeTodaysPTPAccountsMessage(allocatedLoanAccounts, currentDate.Date);
        }

        /// <summary>
        /// Retrieves the list of allocated loan accounts for a specified collector user within a given month and year.
        /// </summary>
        /// <param name="collectorUserId">The ID of the collector user whose loan accounts are to be fetched.</param>
        /// <param name="month">The month for which to fetch the allocated loan accounts.</param>
        /// <param name="year">The year for which to fetch the allocated loan accounts.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="List{LoanAccountDtoWithId}"/> result containing the allocated loan accounts.</returns>
        private async Task<List<LoanAccountDtoWithId>> GetAllocatedLoanAccountsAsync(string collectorUserId, int month, int year)
        {
            var query = _repoFactory.GetRepo().FindAll<LoanAccount>()
                             .ByCollectorId(collectorUserId)
                             .WithMonthAndYear(month, year);

            return await query.SelectTo<LoanAccountDtoWithId>().ToListAsync();
        }

        /// <summary>
        /// Retrieves the list of feedback account IDs that have activity after the specified start date.
        /// </summary>
        /// <param name="accountIds">The list of account IDs to filter by.</param>
        /// <param name="startDate">The start date for filtering feedback activity.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="List{string}"/> result containing the feedback account IDs.</returns>
        private async Task<List<string>> GetFeedbackAccountIdsWithActivityAsync(List<string> accountIds, DateTime startDate)
        {
            return await _repoFactory.GetRepo().FindAll<Feedback>()
                 .ByFeedbackCreateddate(startDate)
                 .ByFeedbackAccountIds(accountIds)
                 .Select(feedback => feedback.AccountId)
                 .Distinct()
                 .ToListAsync();
        }

        /// <summary>
        /// Composes a message for accounts that have not been attempted (no trail) based on the unattempted account IDs.
        /// </summary>
        /// <param name="allocatedAccounts">The list of allocated loan accounts.</param>
        /// <param name="unattemptedAccountIds">The list of unattempted account IDs.</param>
        /// <returns>A string containing the formatted message for unattempted accounts.</returns>
        private string ComposeUnattemptedAccountsMessage(List<LoanAccountDtoWithId> allocatedAccounts, List<string> unattemptedAccountIds)
        {
            var unattemptedAccounts = allocatedAccounts.Where(account => unattemptedAccountIds.Contains(account.Id)).ToList();

            decimal totalOutstanding = CalculateTotalAmountDue(unattemptedAccounts);
            string formattedAmount = totalOutstanding.ToString("N2");

            //return $"You have {unattemptedAccountIds.Count} account(s) with no customer contact (no trail), totaling an outstanding amount of {formattedAmount}. Please contact the customer and ensure the trail is updated.";
            return $"You have {unattemptedAccountIds.Count} account(s) with no attempts having an Outstanding of ₹{formattedAmount}. Please contact the customer and remember to update the trail.";
        }

        /// <summary>
        /// Composes a message for accounts with a scheduled PTP (Promise to Pay) date for today.
        /// </summary>
        /// <param name="allocatedAccounts">The list of allocated loan accounts.</param>
        /// <param name="today">The current date to compare the PTP date against.</param>
        /// <returns>A string containing the formatted message for accounts with today's PTP date.</returns>
        private string ComposeTodaysPTPAccountsMessage(List<LoanAccountDtoWithId> allocatedAccounts, DateTime today)
        {
            var todaysPTPAccounts = allocatedAccounts
                 .Where(account => account.LatestPTPDate?.Date == today)
                 .ToList();

            decimal totalPTPAmount = CalculateTotalAmountDue(todaysPTPAccounts);
            string formattedPTPAmount = totalPTPAmount.ToString("N2");

            //return $"There are {todaysPTPAccounts.Count} account(s) with a PTP date scheduled for today, totaling an outstanding amount of {formattedPTPAmount}. Please follow up with the customers and initiate collection.";
            return $"There are {todaysPTPAccounts.Count} account(s) with PTP date is equal to today's date having an Outstanding of ₹{formattedPTPAmount}. Please contact the customer and collect the pending dues.";
        }

        /// <summary>
        /// Calculates the total outstanding amount due for a list of loan accounts, distinguishing between credit card and loan products.
        /// </summary>
        /// <param name="accounts">The list of loan accounts to calculate the outstanding amounts for.</param>
        /// <returns>The total outstanding amount due across all the provided loan accounts.</returns>
        private decimal CalculateTotalAmountDue(List<LoanAccountDtoWithId> accounts)
        {
            decimal totalCreditCardDue = accounts
                 .Where(account => account.ProductCode == "CreditCard")
                 .Sum(account => account.CURRENT_TOTAL_AMOUNT_DUE ?? 0);

            decimal totalLoanOutstanding = accounts
                 .Where(account => account.ProductCode != "CreditCard")
                 .Sum(account => account.CURRENT_POS ?? 0);

            return totalCreditCardDue + totalLoanOutstanding;
        }
        /// <summary>
        /// Sets default trail gap and today's PTP messages in the notification DTO
        /// when no allocated loan accounts are found for the collector.
        /// </summary>
        /// <param name="notificationDto">
        /// The DTO object that holds user notification messages such as trail gap and today's PTP info.
        /// </param>
        private void SetDefaultMessages(GetUserNotificationDto notificationDto)
        {
            notificationDto.TrailGapMessage =
                "You have 0 account(s) with no attempts having an Outstanding of ₹0. Please contact the customer and remember to update the trail.";

            notificationDto.TodaysPTPMessage =
                "There are 0 account(s) with PTP date equal to today's date having an Outstanding of ₹0. Please contact the customer and collect the pending dues.";
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class GetUserNotificationParams : DtoBridge
    {

    }
}
