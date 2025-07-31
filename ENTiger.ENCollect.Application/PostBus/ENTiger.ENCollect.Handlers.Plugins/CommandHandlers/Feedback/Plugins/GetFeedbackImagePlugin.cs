using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFeedbackImagePlugin : FlexiPluginBase, IFlexiPlugin<GetFeedbackImagePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13668846fa29c635a9640d037af79a";
        public override string FriendlyName { get; set; } = "GetFeedbackImagePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetFeedbackImagePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetFeedbackImagePlugin(ILogger<GetFeedbackImagePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetFeedbackImagePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}