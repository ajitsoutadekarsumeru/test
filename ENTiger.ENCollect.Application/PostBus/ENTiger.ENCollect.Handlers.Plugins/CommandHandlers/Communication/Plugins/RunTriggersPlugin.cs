using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class RunTriggersPlugin : FlexiPluginBase, IFlexiPlugin<RunTriggersPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1b1fa279ee4ad32d90e5252ba1e0c0";
        public override string FriendlyName { get; set; } = "RunTriggersPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<RunTriggersPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICommunicationTriggerRepository _triggerRepo;
        private readonly AccountFetchStrategyFactory _strategyFactory;

        protected CommunicationTrigger? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private IReadOnlyList<string> actorIds;

        public RunTriggersPlugin(ILogger<RunTriggersPlugin> logger,
            IFlexHost flexHost,
            IRepoFactory repoFactory,
            ICommunicationTriggerRepository triggerRepo,
            AccountFetchStrategyFactory strategyFactory
            )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _triggerRepo = triggerRepo;
            _strategyFactory = strategyFactory;
        }

        public virtual async Task Execute(RunTriggersPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var triggers = await _triggerRepo.GetAllActiveAsync(_flexAppContext);
            if (triggers == null || !triggers.Any())
            {
                _logger.LogWarning("No active triggers found.");
                return;
            }

            foreach (var trigger in triggers)
            {
                    // Pick the appropriate strategy for this trigger type
                    var strategy = _strategyFactory.Get(trigger.TriggerType.CustomName);

                    actorIds = await strategy.IdentifyActorsAsync(trigger, _flexAppContext);
                    if (trigger.TriggerType.EntryPoint == EntryPointEnum.Account.Value)
                    {
                        // Enqueue account IDs
                        await _triggerRepo.EnqueueRangeAsync(
                            _flexAppContext,
                            packet.Cmd.Dto.GetGeneratedId(),
                            trigger.Id,
                            actorIds
                        );
                        EventCondition = CONDITION_ONACCOUNT;
                    }
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}