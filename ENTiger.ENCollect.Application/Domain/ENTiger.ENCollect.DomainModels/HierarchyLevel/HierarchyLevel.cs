using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class HierarchyLevel : PersistenceModelBridge
{
    protected readonly ILogger<HierarchyLevel> _logger;

    protected HierarchyLevel()
    {
        _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<HierarchyLevel>>();
    }

    public HierarchyLevel(ILogger<HierarchyLevel> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(50)]
    public string Name { get; set; }
    public int Order { get; set; }
    
    [StringLength(50)]
    public string Type { get; set; }

    // Navigation: all items at this level
    public ICollection<HierarchyMaster> Items { get; set; } = new List<HierarchyMaster>();
    #endregion

    #endregion

    #region "Private Methods"
    #endregion

}
