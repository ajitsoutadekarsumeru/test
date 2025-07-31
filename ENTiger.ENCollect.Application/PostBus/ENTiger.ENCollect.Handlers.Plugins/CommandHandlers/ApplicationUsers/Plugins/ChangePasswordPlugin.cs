using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ChangePasswordPlugin : FlexiPluginBase, IFlexiPlugin<ChangePasswordPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a143e5b8d1ce72db7476081b8d7e215";
        public override string FriendlyName { get; set; } = "ChangePasswordPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ChangePasswordPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ChangePasswordPlugin(ILogger<ChangePasswordPlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ChangePasswordPostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}