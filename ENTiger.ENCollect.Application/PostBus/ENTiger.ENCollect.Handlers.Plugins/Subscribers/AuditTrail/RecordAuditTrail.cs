using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AuditTrailModule
{
    public partial class RecordAuditTrail : IRecordAuditTrail
    {
        protected readonly ILogger<RecordAuditTrail> _logger;
        protected string EventCondition = "";
        protected FlexAppContextBridge? _flexAppContext;
        private readonly IAuditTrailManager _auditManager;
        public RecordAuditTrail(ILogger<RecordAuditTrail> logger, IAuditTrailManager auditManager)
        {
            _logger = logger;
            _auditManager = auditManager;
        }

        public virtual async Task Execute(AuditTrailRequestedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            try
            {
                await _auditManager.RecordAuditEvent(@event.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception for AuditTrailRecord: {@Exception}", ex);
            }
            await this.Fire<RecordAuditTrail>(EventCondition, serviceBusContext);
        }
    }
}
