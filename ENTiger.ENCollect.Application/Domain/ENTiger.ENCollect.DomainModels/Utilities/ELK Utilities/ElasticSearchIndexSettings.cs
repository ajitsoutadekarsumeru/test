using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.DomainModels.Utilities.ELK_Utilities;

/// <summary>
/// Configuration class for ElasticSearch index details per tenant.
/// For Saas clients as elastic search connection is same all indexes will be created in same elasticsearch url, 
/// hence we have one json file to configure indexes for different clients
/// For on-premises client indexes will be created wrt environment 
/// </summary>
public class ElasticSearchIndexSettings
{
    /// <summary>
    /// A list of index details for different tenants is provided
    /// </summary>
    public List<TenantIndexDetails> IndexNames { get; set; } = new List<TenantIndexDetails>();
}

/// <summary>
/// Represents index details for a specific tenant.
/// </summary>
public class TenantIndexDetails
{
    /// <summary>
    /// TenantId should not change from environment to environment, as there is only one Elasticsearch configuration file. 
    /// </summary>
    [Required]
    public string? TenantId { get; set; }

    /// <summary>
    /// Index name of primary allocation insight should not change from environment to environment, 
    /// as there is only one Elasticsearch configuration file.
    /// </summary>
    [Required]
    public string? PrimaryAllocationInsightIndex { get; set; }

    /// <summary>
    /// Index name of secondary allocation insight should not change from environment to environment, 
    /// as there is only one Elasticsearch configuration file.
    /// </summary>
    [Required]
    public string? SecondaryAllocationInsightIndex { get; set; }

    /// <summary>
    /// Index name of money movement allocation insight should not change from environment to environment, 
    /// as there is only one Elasticsearch configuration file.
    /// </summary>
    [Required]
    public string? MoneyMovementInsightIndex { get; set; }


    /// <summary>
    /// Index name of trail gap allocation insight should not change from environment to environment, 
    /// as there is only one Elasticsearch configuration file.
    /// </summary>
    [Required]
    public string? TrailGapInsightIndex { get; set; }

    /// <summary>
    /// Index name of allocation vs achieved insight and name should not change from environment to environment, 
    /// as there is only one Elasticsearch configuration file.
    /// </summary>
    [Required]
    public string? Allocation_AchievedInsightIndex { get; set; }

    [Required]
    public long BucketInterval { get; set; }

}

