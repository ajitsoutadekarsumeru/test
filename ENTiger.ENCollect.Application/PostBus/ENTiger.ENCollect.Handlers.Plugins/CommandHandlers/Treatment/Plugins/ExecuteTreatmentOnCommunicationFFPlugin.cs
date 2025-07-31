using ENTiger.ENCollect.TreatmentModule;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentOnCommunicationFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentOnCommunicationFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ExecuteTreatmentOnCommunicationFFPlugin(ILogger<ExecuteTreatmentOnCommunicationFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ExecuteFragmentedTreatmentDataPacket packet)
        {
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}