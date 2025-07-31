

namespace ENTiger.ENCollect;

/// <summary>
/// A simple in-memory manager for demonstration or testing.
/// Stores records in a private List.
/// In production, you might implement a DB-backed manager.
/// </summary>
public class InMemoryAuditTrailManager : IAuditTrailManager
{
    private readonly List<AuditTrailRecord> _records = new();
   
    public async Task RecordAuditEvent(AuditEventData data)
    {

        // 1. Create a final record
        var auditTrialRecord = new AuditTrailRecord();
        var record = auditTrialRecord.Create(data);

        // 2. Store it (in memory for now)
        _records.Add(record);
    }

    /// <summary>
    /// For demo/debug only: get the stored records.
    /// In a real manager, you might not expose them directly here.
    /// </summary>
    public IReadOnlyList<AuditTrailRecord> GetRecords() => _records.AsReadOnly();
}