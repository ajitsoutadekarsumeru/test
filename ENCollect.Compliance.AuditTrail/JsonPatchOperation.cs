namespace ENTiger.ENCollect;

/// <summary>
/// Represents a single JSON Patch operation (RFC 6902), adapted to include oldValue for "replace" operations
/// and a custom "recordId" field for collection items.
/// </summary>
public class JsonPatchOperation
{
    public string op { get; set; }
    public string path { get; set; }
    public string Id { get; set; }
    public object oldValue { get; set; }
    public object newValue { get; set; }    
}

