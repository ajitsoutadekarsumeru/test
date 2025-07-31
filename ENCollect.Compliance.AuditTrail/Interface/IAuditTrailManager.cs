namespace ENTiger.ENCollect;

/// <summary>
/// Defines the contract for any manager that can record audit events.
/// You can implement this for a database, external service, etc.
/// </summary>
public interface IAuditTrailManager
{
    /// <summary>
    /// Processes the given event data and produces a final audit record.
    /// The ID and Timestamp are generated automatically.
    /// </summary>
    Task RecordAuditEvent(AuditEventData data);
}