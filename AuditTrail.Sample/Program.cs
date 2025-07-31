namespace ENTiger.ENCollect;

/// <summary>
/// Sample program demonstrating how to use the AuditTrail library.
/// Typically you'd have your own main app with DI, DB connections, etc.
/// </summary>
internal static class Program
{
    public static void Main()
    {
        // 1. Create an audit manager (in-memory in this demo).
        IAuditTrailManager auditManager = new InMemoryAuditTrailManager();

        // 2. Suppose we have an "old" object and a "new" object to compare.
        //    For demonstration, let's create a simple anonymous object or small class.
        var oldObject = new { Id = 101, Name = "John Old", Email = "john@old.com" };
        var newObject = new { Id = 101, Name = "John New", Email = "john@new.com" };

        // 3. Create an DiffGenerator.
        IDiffGenerator diffGenerator = new DiffGenerator();
        string jsonPatch = diffGenerator.GenerateDiff(oldObject, newObject);

        // 3. Construct the data for an "Edit" operation.
        var eventData = new AuditEventData
        (
            EntityId: "101",
            EntityType: AuditedEntityTypeEnum.Agent.Value, // e.g., "Agent" from the enum
            Operation: AuditOperationEnum.Edit.Value,          
            JsonPatch: jsonPatch,
            InitiatorId: "user123",
            TenantId: "1",
            ClientIP: "10.8.11.171"
        );

        // 4. Record the audit event
        //Add IAuditTrailManager _auditManager in constructor
        auditManager.RecordAuditEvent(eventData);

        // 5. (Demo only) Retrieve the record from the in-memory manager.
        if (auditManager is InMemoryAuditTrailManager inMemory)
        {
            var savedRecords = inMemory.GetRecords();
            foreach (var record in savedRecords)
            {
                Console.WriteLine($"Audit ID: {record.Id}");
                Console.WriteLine($"Time UTC: {record.CreatedDate:o}");
                Console.WriteLine($"Entity: {record.EntityType} ({record.EntityId})");
                Console.WriteLine($"Operation: {record.Operation}");
                Console.WriteLine($"Initiator: {record.CreatedDate}");
                Console.WriteLine($"Diff JSON: {record.DiffJson}");
                Console.WriteLine(new string('-', 50));
            }
        }
    }
}