using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTreatmentReportFilePlugin : FlexiPluginBase, IFlexiPlugin<GetTreatmentReportFilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a15178acdec7c32029a41b45c917c45";
        public override string FriendlyName { get; set; } = "GetTreatmentReportFilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetTreatmentReportFilePlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public GetTreatmentReportFilePlugin(ILogger<GetTreatmentReportFilePlugin> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(GetTreatmentReportFilePostBusDataPacket packet)
        {
            //Write your code here:

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}