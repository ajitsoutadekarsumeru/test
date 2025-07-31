using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
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

        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("UpdateLoanAccount : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            var eventmodel = @event;

            Collection? collection = await FetchCollectionAsync(eventmodel.Id);
            LoanAccount? UpdateAccount = await FetchAccountAsync(collection?.AccountId);

            UpdateAccount.LatestEmailId = collection.EMailId;
            UpdateAccount.LatestMobileNo = collection.MobileNo;
            UpdateAccount.Latest_Number_From_Receipt = collection.MobileNo;
            if (collection.CollectionWorkflowState.GetType().Name == "CollectionAcknowledged")
            {
                UpdateAccount.Paid = true;
                UpdateAccount.Attempted = true;
                UpdateAccount.LatestPaymentDate = collection.CreatedDate.DateTime;
            }
            UpdateAccount.SetAddedOrModified();

            _repoFactory.GetRepo().InsertOrUpdate(UpdateAccount);
            int records = await _repoFactory.GetRepo().SaveAsync();

            _logger.LogInformation("UpdateLoanAccount : End");
        }

        private async Task<LoanAccount?> FetchAccountAsync(string id)
        {
            return await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        private async Task<Collection?> FetchCollectionAsync(string Id)
        {
            return await _repoFactory.GetRepo().FindAll<Collection>().IncludeCollectionWorkflowState().Where(i => i.Id == Id).FirstOrDefaultAsync();
        }
    }
}