using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUnAllocationFilePlugin : FlexiPluginBase, IFlexiPlugin<GetUnAllocationFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1816dccf985ea0ee749762a8a0dca1";
        public override string FriendlyName { get; set; } = "GetUnAllocationFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetUnAllocationFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetUnAllocationFilePlugin(ILogger<GetUnAllocationFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetUnAllocationFilePostBusDataPacket packet)
        {
            //Write your code here:



            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);                        

        }
    }
}