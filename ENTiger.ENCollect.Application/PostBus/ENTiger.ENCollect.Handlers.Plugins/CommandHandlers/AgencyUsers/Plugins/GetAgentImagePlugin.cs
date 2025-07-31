using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentImagePlugin : FlexiPluginBase, IFlexiPlugin<GetAgentImagePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13ba418644c51ed4719f84835fd0c6";
        public override string FriendlyName { get; set; } = "GetAgentImagePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetAgentImagePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetAgentImagePlugin(ILogger<GetAgentImagePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetAgentImagePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}