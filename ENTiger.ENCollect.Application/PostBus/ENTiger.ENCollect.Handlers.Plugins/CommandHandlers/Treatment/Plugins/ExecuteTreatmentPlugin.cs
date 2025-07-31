using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ExecuteTreatmentPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteTreatmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1481fffc3e6b8d68fb461e54ef9c17";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ExecuteTreatmentPlugin(ILogger<ExecuteTreatmentPlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ExecuteTreatmentPostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}