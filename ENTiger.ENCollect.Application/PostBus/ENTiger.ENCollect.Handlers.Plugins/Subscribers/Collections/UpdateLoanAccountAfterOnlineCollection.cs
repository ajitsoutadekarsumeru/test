using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class UpdateLoanAccountAfterOnlineCollection : IUpdateLoanAccountAfterOnlineCollection
    {
        protected readonly ILogger<UpdateLoanAccountAfterOnlineCollection> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateLoanAccountAfterOnlineCollection(ILogger<UpdateLoanAccountAfterOnlineCollection> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(OnlineCollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("UpdateLoanAccountAfterOnlineCollection : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            var eventmodel = @event;

            Collection? collection = await _repoFactory.GetRepo().FindAll<Collection>().Where(i => i.Id == eventmodel.CollectionId).FirstOrDefaultAsync();

            LoanAccount? account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(i => i.Id == collection.AccountId).FirstOrDefaultAsync();

            account.LatestEmailId = collection.EMailId;
            account.LatestMobileNo = collection.MobileNo;
            account.Latest_Number_From_Send_Payment = collection.MobileNo;
            account.SetAddedOrModified();
            account.SetLastModifiedBy(collection.CreatedBy);

            _repoFactory.GetRepo().InsertOrUpdate(account);
            int records = await _repoFactory.GetRepo().SaveAsync();

            _logger.LogInformation("UpdateLoanAccountAfterOnlineCollection : End");
            await Task.CompletedTask;
        }
    }
}