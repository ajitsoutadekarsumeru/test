using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class BranchGeoMap : PersistenceModelBridge
{
    protected readonly ILogger<BranchGeoMap> _logger;

    protected BranchGeoMap()
    {
    }

    public BranchGeoMap(ILogger<BranchGeoMap> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public" 
    [StringLength(32)]
    public string BranchId { get; set; }
    public virtual BaseBranch Branch { get; set; }

    [StringLength(32)]
    public string HierarchyId { get; set; }  // This can be City, State, Region, or Country
    public virtual HierarchyMaster Hierarchy { get; set; }

    // Optional: Add hierarchy level for clarity and indexing
    public int? HierarchyLevel { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}
