using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAllocationFilePlugin : FlexiPluginBase, IFlexiPlugin<GetAllocationFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1816dc5b5873369f31d8ca0d58d9ff";
        public override string FriendlyName { get; set; } = "GetAllocationFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetAllocationFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetAllocationFilePlugin(ILogger<GetAllocationFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetAllocationFilePostBusDataPacket packet)
        {
            //Write your code here:



            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);                        

        }
    }
}