using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Security;
using Elastic.Clients.Elasticsearch;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect;

public partial class AccountContactHistory : PersistenceModelBridge
{
    protected readonly ILogger<AccountContactHistory> _logger;
    public AccountContactHistory() { }
    public AccountContactHistory(ILogger<AccountContactHistory> logger)
    {
        _logger = logger;
    }
    public AccountContactHistory(string contactValue,
        decimal? latitude, decimal? longitude,
        ContactSourceEnum contactSource, ContactTypeEnum contactType, string accountId, string userId)
    {
        Id = SequentialGuid.NewGuidString();
        Latitude = latitude;
        Longitude = longitude;
        ContactValue = contactValue;
        ContactSource = contactSource;
        ContactType = contactType;
        AccountId = accountId;
        CreatedBy = userId;
        LastModifiedBy = userId;
        TrackingState = TrackingState.Added;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(200)]
    public string ContactValue { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Longitude { get; set; }
    public ContactSourceEnum ContactSource { get; set; }
    public ContactTypeEnum ContactType { get; set; }
    public LoanAccount Account { get; set; }
    [StringLength(32)]
    public string? AccountId { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}