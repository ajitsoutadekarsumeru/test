using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersFilePlugin : FlexiPluginBase, IFlexiPlugin<GetUsersFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a15c01fca4c2817cad355b62394e3a3";
        public override string FriendlyName { get; set; } = "GetUsersFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetUsersFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetUsersFilePlugin(ILogger<GetUsersFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetUsersFilePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}