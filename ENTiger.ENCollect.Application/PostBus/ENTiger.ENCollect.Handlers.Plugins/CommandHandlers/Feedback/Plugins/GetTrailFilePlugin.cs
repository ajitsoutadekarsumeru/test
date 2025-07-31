using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTrailFilePlugin : FlexiPluginBase, IFlexiPlugin<GetTrailFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1816dd29d0f05910257435d70bc5cf";
        public override string FriendlyName { get; set; } = "GetTrailFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetTrailFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetTrailFilePlugin(ILogger<GetTrailFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetTrailFilePostBusDataPacket packet)
        {
            //Write your code here:



            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);                        

        }
    }
}