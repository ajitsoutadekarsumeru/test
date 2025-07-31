using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UploadFilePlugin : FlexiPluginBase, IFlexiPlugin<UploadFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a135d1c6b82eafdcd64c58a9da97c1c";
        public override string FriendlyName { get; set; } = "UploadFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UploadFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UploadFilePlugin(ILogger<UploadFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(UploadFilePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}