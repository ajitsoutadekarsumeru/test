using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class UpdateToClientForDepositSlipCreated : IUpdateToClientForDepositSlipCreated
    {
        protected readonly ILogger<UpdateToClientForDepositSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateToClientForDepositSlipCreated(ILogger<UpdateToClientForDepositSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DepositSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<UpdateToClientForDepositSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}