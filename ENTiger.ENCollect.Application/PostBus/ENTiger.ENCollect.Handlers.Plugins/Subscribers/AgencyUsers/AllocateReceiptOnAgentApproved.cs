using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AllocateReceiptOnAgentApproved : IAllocateReceiptOnAgentApproved
    {
        protected readonly ILogger<AllocateReceiptOnAgentApproved> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;
        protected FlexAppContextBridge? _flexAppContext;
        private ReceiptWorkflowState _state;

        public AllocateReceiptOnAgentApproved(ILogger<AllocateReceiptOnAgentApproved> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _state = host.GetFlexStateInstance<ReceiptAllocatedToCollector>();
            _customUtility = customUtility;
        }

        public virtual async Task Execute(AgentApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            ReceiptWorkflowState _state = new ReceiptAllocatedToCollector();
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("AllocateReceiptOnAgentApproved : Start");
            string loggedInUser = _flexAppContext.UserId;

            foreach (var id in @event.Ids)
            {
                var receipts = await _repoFactory.GetRepo().FindAll<Receipt>().ByReceiptCollector(id).ReceiptWorkflowState(_state).ToListAsync();

                if (receipts == null || receipts.Count <= 0)
                {
                    Receipt receipt = new Receipt();
                    receipt.CollectorId = id;
                    receipt.CustomId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.EReceipt.Value);
                    receipt.SetCreatedBy(loggedInUser);
                    receipt.SetLastModifiedBy(loggedInUser);
                    receipt.SetAdded();
                    receipt.MarkAsAllocatedToCollector(loggedInUser);

                    _logger.LogInformation("AllocateReceiptOnAgentApproved : User -  " + id + "| Receipt - " + receipt.CustomId);
                    _repoFactory.GetRepo().InsertOrUpdate(receipt);
                    int records = await _repoFactory.GetRepo().SaveAsync();

                    if (records > 0)
                    {
                        _logger.LogInformation("{Entity} with {EntityId} inserted into Database: ", typeof(Receipt).Name, receipt.CustomId);
                    }
                    else
                    {
                        _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Receipt).Name, receipt.CustomId);
                    }
                }
                else
                {
                    _logger.LogWarning("AllocateReceiptOnAgentApproved : Receipts already exists for the User : " + id + " | Count : " + receipts.Count);
                }
            }
            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic
            //EventCondition = CONDITION_ONSUCCESS;

            _logger.LogInformation("AllocateReceiptOnAgentApproved : End");
            await Task.CompletedTask;
        }
    }
}