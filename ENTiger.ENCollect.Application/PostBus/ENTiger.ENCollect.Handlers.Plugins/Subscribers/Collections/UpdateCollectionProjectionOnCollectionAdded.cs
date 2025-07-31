using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule;

public partial class UpdateCollectionProjectionOnCollectionAdded : IUpdateCollectionProjectionOnCollectionAdded
{
    private readonly ILogger<UpdateCollectionProjectionOnCollectionAdded> _logger;
    private readonly IRepoFactory _repoFactory;

    private FlexAppContextBridge? _flexAppContext;
    private const string EventCondition = ""; // Event condition

    public UpdateCollectionProjectionOnCollectionAdded(
        ILogger<UpdateCollectionProjectionOnCollectionAdded> logger,
        IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;

    }

    public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
    {
        _flexAppContext = @event.AppContext; // Do not remove this line
        _repoFactory.Init(@event);

        // Extract collection IDs from event
        var collectionId = @event.Id;

        // Step 1: Retrieve collection records that match the IDs from the event
        var collectionObj = _repoFactory.GetRepo()
            .FindAll<Collection>()
            .Where(c => c.Id == collectionId)
            .Select(c => new
            {
                c.Id,
                c.CustomId,
                c.AccountId,
                c.Amount,
                c.Status,
                c.CreatedBy

            })
            .FirstOrDefault();

        // var accountIds = collections.Select(c => c.AccountId).Distinct().ToList();

        var repo = _repoFactory.GetRepo();
        var loanAccountDetailsobj = repo.FindAll<LoanAccount>()

                .ByAccountId(collectionObj.AccountId).FirstOrDefault();



        if (loanAccountDetailsobj != null)
        {

            // Create new collection projection record
            var newCollectionProjection = new CollectionProjection
            {
                CollectionId = collectionId,
                BUCKET = loanAccountDetailsobj.BUCKET,
                CURRENT_BUCKET = loanAccountDetailsobj.CURRENT_BUCKET,
                NPA_STAGEID = loanAccountDetailsobj.NPA_STAGEID,
                AgencyId = loanAccountDetailsobj.AgencyId,
                CollectorId = loanAccountDetailsobj.CollectorId,
                TeleCallingAgencyId = loanAccountDetailsobj.TeleCallingAgencyId,
                TeleCallerId = loanAccountDetailsobj.TeleCallerId,

                AllocationOwnerId = loanAccountDetailsobj.AllocationOwnerId,

                BOM_POS = loanAccountDetailsobj.BOM_POS,
                CURRENT_POS = loanAccountDetailsobj.CURRENT_POS,
                PAYMENTSTATUS = collectionObj.Status,
                CURRENT_TOTAL_AMOUNT_DUE = loanAccountDetailsobj.CURRENT_TOTAL_AMOUNT_DUE
            };
            newCollectionProjection.SetAddedOrModified();
            repo.InsertOrUpdate(newCollectionProjection);
        }


        try
        {
            // Step 3: Commit all changes to the database in a single transaction
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                // Log success if records were updated
                _logger.LogDebug("{Entity} updates committed successfully in the database.", nameof(CollectionProjection));
            }
            else
            {
                // Log a warning if nothing was updated
                _logger.LogWarning("No records were updated in the database.");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateCollectionProjectionOnCollectionAdded " + ex.Message + " " + ex.InnerException + " " + ex.StackTrace + " " + ex.Source);
            //throw;
        }


    }
}
