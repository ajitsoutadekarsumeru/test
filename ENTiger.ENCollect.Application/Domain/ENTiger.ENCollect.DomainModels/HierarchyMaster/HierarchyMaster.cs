using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class HierarchyMaster : PersistenceModelBridge
{
    protected readonly ILogger<HierarchyMaster> _logger;

    protected HierarchyMaster()
    {
    }

    public HierarchyMaster(ILogger<HierarchyMaster> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(50)]
    public string Item { get; set; }

    // foreign‐key into HierarchyLevel
    [StringLength(32)]
    public string LevelId { get; set; }
    public HierarchyLevel Level { get; set; }

    // adjacency list: parent/master‐item
    [StringLength(32)]
    public string? ParentId { get; set; }
    public HierarchyMaster Parent { get; set; }

    // navigation to children
    public ICollection<HierarchyMaster> Children { get; set; } = new List<HierarchyMaster>();
    #endregion

    #endregion

    #region "Private Methods"
    #endregion

}
