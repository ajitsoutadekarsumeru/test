using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class UserBucketScope : PersistenceModelBridge
{
    protected readonly ILogger<UserBucketScope> _logger;

    protected UserBucketScope()
    {
    }

    public UserBucketScope(ILogger<UserBucketScope> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(32)]
    public string? BucketScopeId { get; set; }
    public Bucket? BucketScope { get; set; }

    [StringLength(32)]
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}