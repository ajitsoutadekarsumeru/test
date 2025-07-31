using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class UserProductScope : PersistenceModelBridge
{
    protected readonly ILogger<UserProductScope> _logger;

    protected UserProductScope()
    {
    }

    public UserProductScope(ILogger<UserProductScope> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(32)]
    public string? ProductScopeId { get; set; }
    public HierarchyMaster? ProductScope { get; set; }

    [StringLength(32)]
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}