using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class AllocateReceiptOnCompanyUserApproved : IAllocateReceiptOnCompanyUserApproved
    {
        protected readonly ILogger<AllocateReceiptOnCompanyUserApproved> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private ReceiptWorkflowState _state;
        protected readonly ICustomUtility _customUtility;

        public AllocateReceiptOnCompanyUserApproved(ILogger<AllocateReceiptOnCompanyUserApproved> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _state = host.GetFlexStateInstance<ReceiptAllocatedToCollector>();
            _customUtility = customUtility;
        }

        public virtual async Task Execute(CompanyUserApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            ReceiptWorkflowState _state = new ReceiptAllocatedToCollector();
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("AllocateReceiptOnCompanyUserApproved : Start");
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

                    _logger.LogInformation("AllocateReceiptOnCompanyUserApproved : User -  " + id + "| Receipt - " + receipt.CustomId);
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
                    _logger.LogWarning("AllocateReceiptOnCompanyUserApproved : Receipts already exists for the User : " + id + " | Count : " + receipts.Count);
                }
            }
            _logger.LogInformation("AllocateReceiptOnCompanyUserApproved : End");
        }
    }
}