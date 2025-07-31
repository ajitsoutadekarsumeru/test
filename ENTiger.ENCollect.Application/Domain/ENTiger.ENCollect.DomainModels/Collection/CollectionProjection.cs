using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect;

/// <summary>
///
/// </summary>

public  class CollectionProjection : PersistenceModel
{
    protected readonly ILogger<Collection> _logger;
    protected readonly IFlexHost _flexHost;

    public CollectionProjection()
    {
        _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
        _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Collection>>();
    }


    public CollectionProjection(ILogger<Collection> logger, IFlexHost flexHost)
    {
        _logger = logger;
        _flexHost = flexHost;
    }

    #region "Attributes"

    #region "Protected"

    [StringLength(32)]
    public string CollectionId { get; set; }
    public Collection Collection { get; set; }

    public long? BUCKET { get; set; }

    [StringLength(50)]
    public string? CURRENT_BUCKET { get; set; }

    [StringLength(50)]
    public string? NPA_STAGEID { get; set; }


    [StringLength(32)]
    public string? AgencyId { get; set; }

    public ApplicationOrg Agency { get; set; }

    [StringLength(32)]
    public string? CollectorId { get; set; }

    public ApplicationUser Collector { get; set; }

    [StringLength(32)]
    public string? TeleCallingAgencyId { get; set; }

    public ApplicationOrg TeleCallingAgency { get; set; }

    [StringLength(32)]
    public string? TeleCallerId { get; set; }

    public ApplicationUser TeleCaller { get; set; }

    [StringLength(32)]
    public string? AllocationOwnerId { get; set; }

    public ApplicationUser AllocationOwner { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? BOM_POS { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CURRENT_POS { get; set; }

    [StringLength(50)]
    public string? PAYMENTSTATUS { get; set; }

    [Column(TypeName = "decimal(16,2)")]
    public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }


    #endregion "Protected"

    #endregion "Attributes"

}