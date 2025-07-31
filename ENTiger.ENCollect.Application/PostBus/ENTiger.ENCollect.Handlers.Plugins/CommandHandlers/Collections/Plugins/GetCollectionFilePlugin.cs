using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetCollectionFilePlugin : FlexiPluginBase, IFlexiPlugin<GetCollectionFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a189a5c432117eab3a35f83d62af15b";
        public override string FriendlyName { get; set; } = "GetCollectionFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetCollectionFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetCollectionFilePlugin(ILogger<GetCollectionFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetCollectionFilePostBusDataPacket packet)
        {
            //Write your code here:



            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);                        

        }
    }
}