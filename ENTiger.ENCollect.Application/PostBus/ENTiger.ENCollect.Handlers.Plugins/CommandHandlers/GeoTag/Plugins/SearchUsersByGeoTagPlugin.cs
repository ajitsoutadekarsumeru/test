using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchUsersByGeoTagPlugin : FlexiPluginBase, IFlexiPlugin<SearchUsersByGeoTagPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13679c3c77922298f5ad5b012ee4c5";
        public override string FriendlyName { get; set; } = "SearchUsersByGeoTagPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SearchUsersByGeoTagPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public SearchUsersByGeoTagPlugin(ILogger<SearchUsersByGeoTagPlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(SearchUsersByGeoTagPostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}