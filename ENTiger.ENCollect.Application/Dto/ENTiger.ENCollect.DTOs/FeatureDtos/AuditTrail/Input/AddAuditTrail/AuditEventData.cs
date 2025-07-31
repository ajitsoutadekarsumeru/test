namespace ENTiger.ENCollect;

public record AuditEventData(
    string EntityId,
    string EntityType,
    string Operation,  
    string JsonPatch,   // Contains the RFC 6902 JSON Patch diff
    string InitiatorId,
    string TenantId,
    string ClientIP
);