using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect;
public class InDbAuditTrailManager : IAuditTrailManager
{
    private readonly List<AuditTrailRecord> _records = new();
    protected readonly ILogger<InDbAuditTrailManager> _logger;
    protected readonly IFlexHost _flexHost;
    protected readonly IRepoFactory _repoFactory;
    public InDbAuditTrailManager(ILogger<InDbAuditTrailManager> logger, IFlexHost flexHost, IRepoFactory repoFactory)
    {
        _logger = logger;
        _flexHost = flexHost;
        _repoFactory = repoFactory;
    }
    public async Task RecordAuditEvent(AuditEventData data)
    {
        FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
        {
            TenantId = data.TenantId,
            UserId = data.InitiatorId
        };
        _repoFactory.Init(hostContextInfo);

        var record = _flexHost.GetDomainModel<AuditTrailRecord>().Create(data);

        _repoFactory.GetRepo().InsertOrUpdate(record);
        int records = await _repoFactory.GetRepo().SaveAsync();

        if (records > 0)
            _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Object).Name, record.Id);
        else
            _logger.LogWarning("No records inserted for AuditTrailRecord");
    }

    public IReadOnlyList<AuditTrailRecord> GetRecords() => _records.AsReadOnly();
}