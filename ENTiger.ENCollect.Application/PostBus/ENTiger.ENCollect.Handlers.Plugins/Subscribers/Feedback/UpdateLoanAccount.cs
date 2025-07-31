using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class UpdateLoanAccount : IUpdateLoanAccount
    {
        protected readonly ILogger<UpdateLoanAccount> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateLoanAccount(ILogger<UpdateLoanAccount> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(FeedbackAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            LoanAccount account = await BuildAsync(@event.Id);

            _repoFactory.GetRepo().InsertOrUpdate(account);
            int records = await _repoFactory.GetRepo().SaveAsync();

            await this.Fire<UpdateLoanAccount>(EventCondition, serviceBusContext);
        }

        private async Task<LoanAccount> BuildAsync(string id)
        {
            _logger.LogInformation("UpdateLoanAccount : Build - Start");

            Feedback? feedback = await _repoFactory.GetRepo().FindAll<Feedback>().Where(x => x.Id == id).FirstOrDefaultAsync();

            LoanAccount?  account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => x.Id == feedback.AccountId).FirstOrDefaultAsync();

            if (feedback?.DispositionCode?.ToLower() == "ptp")
            {
                account.LatestPTPDate = feedback.PTPDate ?? account.LatestPTPDate;
            }

            account.LatestFeedbackId = feedback.Id;
            account.LatestPTPAmount = feedback.PTPAmount;
            account.DispCode = feedback.DispositionCode;
            account.GroupName = feedback.DispositionGroup;
            account.LatestFeedbackDate = feedback.FeedbackDate ?? account.LatestFeedbackDate;
            account.LatestLatitude = feedback.Latitude != null ? Convert.ToString(feedback.Latitude) : string.Empty;
            account.LatestLongitude = feedback.Longitude != null ? Convert.ToString(feedback.Longitude) : string.Empty;
            account.SetLastModifiedDate(DateTimeOffset.Now);

            if (!string.IsNullOrEmpty(feedback.NewContactNo))
            {
                _logger.LogInformation("UpdateLoanAccount : NewContactNo : {NewContactNo}", feedback.NewContactNo);
                account.LatestMobileNo = feedback.NewContactNo;
                account.Latest_Number_From_Trail = feedback.NewContactNo;
            }
            if (!string.IsNullOrEmpty(feedback.NewEmailId))
            {
                _logger.LogInformation("UpdateLoanAccount : NewEmailId : {NewEmailId}", feedback.NewEmailId);
                account.Latest_Email_From_Trail = feedback.NewEmailId;
            }
            if (!string.IsNullOrEmpty(feedback.NewAddress))
            {
                _logger.LogInformation("UpdateLoanAccount : NewAddress : {NewAddress}", feedback.NewAddress);
                account.Latest_Address_From_Trail = feedback.NewAddress;
            }
            if (!string.IsNullOrEmpty(feedback.AssignTo))
            {
                _logger.LogInformation("UpdateLoanAccount : AssignTo : {AssignTo}", feedback.AssignTo);
                account.CollectorId = feedback.AssignTo;
                account.LatestAllocationDate = DateTime.Now;
                account = FetchUserOrgId(feedback.AssignTo, account);
            }

            if (account.Paid != true)
            {
                _logger.LogInformation("UpdateLoanAccountHandler : Paid - False");
                account.Attempted = true;
            }
            else
            {
                account.Attempted = true;
            }

            _logger.LogInformation("UpdateLoanAccount : Build - End");
            return account;
        }
        private LoanAccount FetchUserOrgId(string userId, LoanAccount account)
        {
            string baseBranchId = string.Empty;

            ApplicationUser user = _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == userId).FirstOrDefault();

            if (user == null)
            {
                _logger.LogWarning("User not found for userId: {UserId}", userId);
                return account;
            }

            if (user is CompanyUser companyUser)
            {
                account.CollectorId = userId;
                account.AgencyId = (user as CompanyUser)?.BaseBranchId;
            }
            else if (user is AgencyUser agencyUser)
            {
                if (agencyUser.AgencyId == account.AgencyId)
                {
                    account.CollectorId = userId;
                }
                else
                {
                    _logger.LogWarning("AgencyUser {UserId} does not match with account AgencyId {AgencyId}.", userId, account.AgencyId);
                }
            }

            return account;
        }
    }
}