using ENTiger.ENCollect.DomainModels.ExtensionMethods;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule;

public partial class UpdateLoanaccountsProjectionOnCollectionAdded : IUpdateLoanAccountsProjectionOnAddCollection
{
    private readonly ILogger<UpdateLoanaccountsProjectionOnCollectionAdded> _logger;
    private readonly IRepoFactory _repoFactory;

    private FlexAppContextBridge? _flexAppContext;
    private const string EventCondition = ""; // Event condition

    public UpdateLoanaccountsProjectionOnCollectionAdded(
        ILogger<UpdateLoanaccountsProjectionOnCollectionAdded> logger,
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
                c.CollectionDate,
                c.CollectionMode
            })
            .FirstOrDefault();

        // var accountIds = collections.Select(c => c.AccountId).Distinct().ToList();

        var repo = _repoFactory.GetRepo();
        var projectionObj = repo.FindAll<LoanAccountsProjection>()
                .ByTransactionMonthAndYear()
                .ByProjectionLoanAccountId(collectionObj.AccountId).FirstOrDefault();



        if (projectionObj != null)
        {
            // Update existing projection
            projectionObj.TotalCollectionAmount += collectionObj.Amount;
            projectionObj.TotalCollectionCount += 1;
            projectionObj.LastCollectionAmount = collectionObj.Amount;
            projectionObj.LastCollectionDate  = collectionObj.CollectionDate;
            projectionObj.LastCollectionMode =collectionObj.CollectionMode;
            projectionObj.Version += 1;
            
            projectionObj.SetAddedOrModified();
            repo.InsertOrUpdate(projectionObj);
        }
        else
        {
            // Create new projection record
            var newTransaction = new LoanAccountsProjection
            {
                LoanAccountId = collectionObj.AccountId,
                TotalCollectionAmount = collectionObj.Amount,
                TotalCollectionCount = 1,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                LastCollectionDate = collectionObj.CollectionDate,
                LastCollectionAmount = collectionObj.Amount,
                LastCollectionMode = collectionObj.CollectionMode,
                TotalTrailCount = 0,
                TotalBPTPCount = 0,
                TotalPTPCount = 0,
                Version = 1
            };
            newTransaction.SetAddedOrModified();
            repo.InsertOrUpdate(newTransaction);
        }


        try
        {
            // Step 3: Commit all changes to the database in a single transaction
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
            _logger.LogError("Exception in UpdateLoanaccountsProjectionOnCollectionAdded " + ex.Message + " " + ex.InnerException + " " + ex.StackTrace + " " + ex.Source);
            throw;
        }

        
    }
}
