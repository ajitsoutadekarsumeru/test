using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect;

/// <summary>
///
/// </summary>
[Table("LoanAccountsProjection")]
public partial class LoanAccountsProjection : DomainModelBridge
{
    protected readonly ILogger<LoanAccount> _logger;

    public LoanAccountsProjection()
    {
    }

    public LoanAccountsProjection(ILogger<LoanAccount> logger)
    {
        _logger = logger;
    }

    #region "Attributes"



    #region "Protected"

    public int? Year { get; set; }

    public int? Month { get; set; }

    [StringLength(32)]
    public string LoanAccountId { get; set; }
    public LoanAccount LoanAccount { get; set; }
    public decimal? TotalCollectionAmount { get; set; }

    public int? TotalCollectionCount { get; set; }

    public DateTime? LastCollectionDate { get; set; }

    public decimal? LastCollectionAmount { get; set; }

    [StringLength(50)]
    public string? LastCollectionMode { get; set; }

    public int? TotalTrailCount { get; set; }

    public int? TotalPTPCount { get; set; }

    public int? TotalBPTPCount { get; set; }
    public DateTime? LatestBPTPDate { get; set; }

    [StringLength(50)]
    public string? CurrentDispositionGroup { get; set; }

    [StringLength(50)]
    public string? CurrentDispositionCode { get; set; }

    public DateTime? CurrentDispositionDate { get; set; }

    public DateTime? CurrentNextActionDate { get; set; }

    [StringLength(50)]
    public string? PreviousDispositionGroup { get; set; }

    [StringLength(50)]
    public string? PreviousDispositionCode { get; set; }

    public DateTime? PreviousDispositionDate { get; set; }

    public DateTime? PreviousNextActionDate { get; set; }

    // Concurrency token
    public int Version { get; set; }

    #endregion "Protected"

    #endregion "Attributes"
}
