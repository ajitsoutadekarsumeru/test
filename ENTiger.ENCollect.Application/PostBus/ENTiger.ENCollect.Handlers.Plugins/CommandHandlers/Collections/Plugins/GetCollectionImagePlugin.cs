using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionImagePlugin : FlexiPluginBase, IFlexiPlugin<GetCollectionImagePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138e3e4472d88ce4c72c1ed98855c2";
        public override string FriendlyName { get; set; } = "GetCollectionImagePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetCollectionImagePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetCollectionImagePlugin(ILogger<GetCollectionImagePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetCollectionImagePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}