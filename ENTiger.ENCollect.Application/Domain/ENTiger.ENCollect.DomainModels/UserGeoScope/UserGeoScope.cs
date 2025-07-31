using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class UserGeoScope : PersistenceModelBridge
{
    protected readonly ILogger<UserGeoScope> _logger;

    protected UserGeoScope()
    {
    }

    public UserGeoScope(ILogger<UserGeoScope> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(32)]
    public string? GeoScopeId { get; set; }
    public HierarchyMaster? GeoScope { get; set; }

    [StringLength(32)]
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}