using Microsoft.Extensions.Logging;
using Sumeru.Flex;


namespace ENTiger.ENCollect.FeedbackModule;

public partial class UpdateFeedbackProjectionOnFeedbackAdded : IUpdateFeedbackProjectionOnFeedbackAdded
{
    private readonly ILogger<UpdateFeedbackProjectionOnFeedbackAdded> _logger;
    private readonly IRepoFactory _repoFactory;

    private FlexAppContextBridge? _flexAppContext;
    private const string EventCondition = ""; // Event condition

    public UpdateFeedbackProjectionOnFeedbackAdded(
        ILogger<UpdateFeedbackProjectionOnFeedbackAdded> logger,
        IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;

    }

    public virtual async Task Execute(FeedbackAddedEvent @event, IFlexServiceBusContext serviceBusContext)
    {
        _flexAppContext = @event.AppContext; // Do not remove this line
        _repoFactory.Init(@event);

        var feedbackId = @event.Id;

        var feedbackObj = _repoFactory.GetRepo()
            .FindAll<Feedback>()
            .Where(c => c.Id == feedbackId)
            .Select(c => new
            {
                c.Id,
                c.AccountId,
                c.DispositionCode,
                c.DispositionGroup,
                c.DispositionDate,
                c.PTPDate,
                c.CreatedDate

            })
            .FirstOrDefault();

        var repo = _repoFactory.GetRepo();
        var loanAccountDetailsobj = repo.FindAll<LoanAccount>()
                .ByAccountId(feedbackObj.AccountId).FirstOrDefault();

        if (loanAccountDetailsobj != null)
        {
            var feedbacksecondlastObj = _repoFactory.GetRepo()
                                     .FindAll<Feedback>()
                                        .OrderByDescending(c => c.CreatedDate)
                                        .Skip(1)
                                        .Select(c => new
                                        {
                                            c.Id,
                                            c.AccountId,
                                            c.DispositionCode,
                                            c.DispositionGroup,
                                            c.DispositionDate,
                                            c.PTPDate,
                                            c.CreatedDate
                                        })
                                        .FirstOrDefault();

            var newFeedbackProjection = new FeedbackProjection
            {
                FeedbackId = feedbackId,
                BUCKET = loanAccountDetailsobj.BUCKET,
                CURRENT_BUCKET = loanAccountDetailsobj.CURRENT_BUCKET,
                NPA_STAGEID = loanAccountDetailsobj.NPA_STAGEID,
                AgencyId = loanAccountDetailsobj.AgencyId,
                CollectorId = loanAccountDetailsobj.CollectorId,
                TeleCallingAgencyId = loanAccountDetailsobj.TeleCallingAgencyId,
                TeleCallerId = loanAccountDetailsobj.TeleCallerId,
                AllocationOwnerId = loanAccountDetailsobj.AllocationOwnerId,
            };

            if (feedbacksecondlastObj != null)
            {
                newFeedbackProjection.LastDispositionDate = feedbacksecondlastObj?.DispositionDate;
                newFeedbackProjection.LastDispositionCode = feedbacksecondlastObj?.DispositionCode;
                newFeedbackProjection.LastDispositionCodeGroup = feedbacksecondlastObj?.DispositionGroup;
                newFeedbackProjection.LastPTPDate = feedbacksecondlastObj?.PTPDate;
            }

            newFeedbackProjection.SetAddedOrModified();
            repo.InsertOrUpdate(newFeedbackProjection);
        }


        try
        {
            // Step 3: Commit all changes to the database in a single transaction
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                // Log success if records were updated
                _logger.LogDebug("{Entity} updates committed successfully in the database.", nameof(FeedbackProjection));
            }
            else
            {
                // Log a warning if nothing was updated
                _logger.LogWarning("No records were updated in the database.");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateFeedbackProjectionOnFeedbackAdded " + ex.Message + " " + ex.InnerException + " " + ex.StackTrace + " " + ex.Source);
            //throw;
        }


    }
}
