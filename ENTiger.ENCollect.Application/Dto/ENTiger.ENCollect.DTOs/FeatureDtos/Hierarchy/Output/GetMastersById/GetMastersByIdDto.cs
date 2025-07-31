namespace ENTiger.ENCollect.HierarchyModule;

public partial class GetMastersByIdDto : DtoBridge
{
    public string Id { get; set; }
    public string LevelId { get; set; }
    public string Name { get; set; }
    public string? ParentId { get; set; }
    public string? ParentName { get; set; }
}
