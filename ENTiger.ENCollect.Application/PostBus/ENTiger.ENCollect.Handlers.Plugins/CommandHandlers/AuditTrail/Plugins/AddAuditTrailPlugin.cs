using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AuditTrailModule
{
    public partial class AddAuditTrailPlugin : FlexiPluginBase, IFlexiPlugin<AddAuditTrailPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1852e1bd24044bc33c417d09344da6";
        public override string FriendlyName { get; set; } = "AddAuditTrailPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddAuditTrailPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        private readonly IAuditTrailManager _auditManager;

        protected FlexAppContextBridge? _flexAppContext;

        public AddAuditTrailPlugin(ILogger<AddAuditTrailPlugin> logger, IFlexHost flexHost, IAuditTrailManager auditManager)
        {
            _logger = logger;
            _flexHost = flexHost;
            _auditManager = auditManager;
        }

        public virtual async Task Execute(AddAuditTrailPostBusDataPacket packet)
        {
            await _auditManager.RecordAuditEvent(packet.Cmd.Data);
        }
    }
}