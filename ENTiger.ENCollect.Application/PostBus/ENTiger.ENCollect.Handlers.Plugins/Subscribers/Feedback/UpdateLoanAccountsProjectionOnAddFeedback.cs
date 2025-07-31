using ENTiger.ENCollect.DomainModels.ExtensionMethods;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
namespace ENTiger.ENCollect.CollectionsModule;

public partial class UpdateLoanAccountsProjectionOnAddFeedback : IUpdateLoanAccountsProjectionOnAddFeedback
{
    private readonly ILogger<UpdateLoanAccountsProjectionOnAddFeedback> _logger;
    private readonly IRepoFactory _repoFactory;

    private FlexAppContextBridge? _flexAppContext;
    private const string EventCondition = ""; // Event condition

    public UpdateLoanAccountsProjectionOnAddFeedback(
        ILogger<UpdateLoanAccountsProjectionOnAddFeedback> logger,
        IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;

    }

    public virtual async Task Execute(FeedbackAddedEvent @event, IFlexServiceBusContext serviceBusContext)
    {
        _flexAppContext = @event.AppContext; // Do not remove this line
        _repoFactory.Init(@event);

        // Extract collection IDs from event
        var feedbackId = @event.Id;

        // Step 1: Retrieve collection records that match the IDs from the event

        var feedbackData = _repoFactory.GetRepo()
            .FindAll<Feedback>()
            .Where(c => c.Id == feedbackId)
            .Select(c => new
            {
                c.Id,
                c.DispositionCode,
                c.DispositionGroup,
                c.NextAction,
                c.DispositionDate,
                c.AccountId,
                c.FeedbackDate,
                c.CreatedDate,
                c.PTPDate
            })
            .FirstOrDefault();

        // Get current month and year for transaction matching
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;

        if (feedbackData != null)
        {
            var repo = _repoFactory.GetRepo();

            // Attempt to retrieve existing transaction record for the loan account and current period

            var feedbackProjection = repo.FindAll<LoanAccountsProjection>()
                 .ByTransactionMonthAndYear()
                 .ByProjectionLoanAccountId(feedbackData.AccountId).FirstOrDefault();

            if (feedbackProjection != null)
            {
                // If record exists, update Feedback field


                feedbackProjection.PreviousDispositionCode = feedbackProjection.CurrentDispositionCode;
                feedbackProjection.PreviousDispositionGroup = feedbackProjection.CurrentDispositionGroup;
                feedbackProjection.PreviousDispositionDate = feedbackProjection.CurrentDispositionDate;
                feedbackProjection.PreviousNextActionDate = feedbackProjection.CurrentNextActionDate;
                feedbackProjection.TotalTrailCount = feedbackProjection.TotalTrailCount + 1;
                feedbackProjection.TotalPTPCount = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.PTP.Value ? feedbackProjection.TotalPTPCount + 1 : feedbackProjection.TotalPTPCount;
                feedbackProjection.TotalBPTPCount = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.BPTP.Value ? feedbackProjection.TotalBPTPCount + 1 : feedbackProjection.TotalBPTPCount;
                feedbackProjection.LatestBPTPDate = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.BPTP.Value ? feedbackData.FeedbackDate : feedbackProjection.LatestBPTPDate;

                feedbackProjection.CurrentDispositionCode = feedbackData.DispositionCode;
                feedbackProjection.CurrentDispositionGroup = feedbackData.DispositionGroup;
                feedbackProjection.CurrentDispositionDate = feedbackData.DispositionDate;
                feedbackProjection.Version = feedbackProjection.Version + 1;

                feedbackProjection.SetAddedOrModified();
                repo.InsertOrUpdate(feedbackProjection);
            }
            else
            {
                // If no record exists, create a new transaction record
                var newTransaction = new LoanAccountsProjection
                {
                    LoanAccountId = feedbackData.AccountId,
                    TotalCollectionAmount = 0,
                    TotalCollectionCount = 0,
                    TotalTrailCount = 1,
                    TotalPTPCount = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.PTP.Value ? 1 : 0,
                    TotalBPTPCount = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.BPTP.Value ? 1 : 0,
                    LatestBPTPDate = feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.BPTP.Value ? feedbackData.FeedbackDate : null,

                    CurrentDispositionCode = feedbackData.DispositionCode,
                    CurrentDispositionGroup = feedbackData.DispositionGroup,
                    CurrentDispositionDate = feedbackData.DispositionDate,

                    PreviousDispositionCode = "",
                    PreviousDispositionGroup = "",
                    Month = currentMonth,
                    Year = currentYear,
                    Version = 1
                };

                if (feedbackData.DispositionGroup?.ToLower() == DispCodeEnum.PTP.Value)
                {
                    newTransaction.CurrentNextActionDate = feedbackData.PTPDate;
                }

                newTransaction.SetAddedOrModified();
                repo.InsertOrUpdate(newTransaction);
            }
        }

        try
        {
            // Step 2: Commit all changes to the database in a single transaction
            int records = await _repoFactory.GetRepo().SaveAsync();

            if (records > 0)
            {
                // Log success if records were updated
                _logger.LogDebug("{Entity} updates committed successfully in the database.", nameof(LoanAccountsProjection));
            }
            else
            {
                // Log a warning if nothing was updated
                _logger.LogWarning("No records were updated in the database.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateLoanAccountsProjectionOnAddFeedback " + ex.Message + " " + ex.InnerException + " " + ex.StackTrace + " " + ex.Source);
            throw;
        }


    }
}
