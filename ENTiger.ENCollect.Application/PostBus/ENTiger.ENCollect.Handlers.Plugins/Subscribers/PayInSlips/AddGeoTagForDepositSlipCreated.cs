using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AddGeoTagForDepositSlipCreated : IAddGeoTagForDepositSlipCreated
    {
        protected readonly ILogger<AddGeoTagForDepositSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoTagForDepositSlipCreated(ILogger<AddGeoTagForDepositSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DepositSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<AddGeoTagForDepositSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}