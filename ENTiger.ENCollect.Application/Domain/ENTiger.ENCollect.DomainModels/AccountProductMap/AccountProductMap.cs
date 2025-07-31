using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class AccountProductMap : PersistenceModelBridge
{
    protected readonly ILogger<AccountProductMap> _logger;

    protected AccountProductMap()
    {
    }

    public AccountProductMap(ILogger<AccountProductMap> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public" 
    [StringLength(32)]
    public string AccountId { get; set; }
    public virtual LoanAccount Account { get; set; }

    [StringLength(32)]
    public string HierarchyId { get; set; }  // This can be Product Group, Product, SubProduct
    public virtual HierarchyMaster Hierarchy { get; set; }

    // Optional: Add hierarchy level for clarity and indexing (e.g., 1 = Product Group, 2 = Product, etc.)
    public int HierarchyLevel { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}