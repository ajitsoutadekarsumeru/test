using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAccountBalanceDetailsPlugin : FlexiPluginBase, IFlexiPlugin<GetAccountBalanceDetailsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a151b485033c51934bcc4780b5e6eb4";
        public override string FriendlyName { get; set; } = "GetAccountBalanceDetailsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetAccountBalanceDetailsPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetAccountBalanceDetailsPlugin(ILogger<GetAccountBalanceDetailsPlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetAccountBalanceDetailsPostBusDataPacket packet)
        {
            //Write your code here:



            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);                        

        }
    }
}