using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipImagePlugin : FlexiPluginBase, IFlexiPlugin<GetPayInSlipImagePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138412c524023ffd80ef6f4fb04d54";
        public override string FriendlyName { get; set; } = "GetPayInSlipImagePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetPayInSlipImagePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetPayInSlipImagePlugin(ILogger<GetPayInSlipImagePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetPayInSlipImagePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}