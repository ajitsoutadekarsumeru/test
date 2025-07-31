using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class AccountGeoMap : PersistenceModelBridge
{
    protected readonly ILogger<AccountGeoMap> _logger;

    protected AccountGeoMap()
    {
    }

    public AccountGeoMap(ILogger<AccountGeoMap> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public" 
    [StringLength(32)]
    public string AccountId { get; set; }
    public virtual LoanAccount Account { get; set; }

    [StringLength(32)]
    public string HierarchyId { get; set; }  // This can be Branch, City, State, Region, or Country
    public virtual HierarchyMaster Hierarchy { get; set; }

    // Optional: Add hierarchy level for clarity and indexing (e.g., 1 = Country, 2 = Region, etc.)
    public int HierarchyLevel { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}