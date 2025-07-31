using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentFlagPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSegmentFlagPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1458ca8f149cc3a4cc87fa3be07d56";
        public override string FriendlyName { get; set; } = "UpdateSegmentFlagPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateSegmentFlagPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UpdateSegmentFlagPlugin(ILogger<UpdateSegmentFlagPlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(UpdateSegmentFlagPostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}