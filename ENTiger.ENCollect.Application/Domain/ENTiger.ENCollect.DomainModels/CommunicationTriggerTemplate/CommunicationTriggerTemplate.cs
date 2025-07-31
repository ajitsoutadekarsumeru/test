using Elastic.Clients.Elasticsearch.Fluent;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class TriggerDeliverySpec : DomainModelBridge
{
    protected readonly ILogger<TriggerDeliverySpec> _logger;
    public TriggerDeliverySpec()
    {
    }
    public TriggerDeliverySpec(ILogger<TriggerDeliverySpec> logger)
    {
        _logger = logger;
    }
    public TriggerDeliverySpec(string triggerId,
      string templateId, string UserId)
    {
        Id = SequentialGuid.NewGuidString();
        CommunicationTriggerId = triggerId;
        CommunicationTemplateId = templateId;
        CreatedBy = UserId;
        TrackingState = TrackingState.Added;
    }
    #region "Attributes"

    #region "Public"
    [StringLength(32)]
    public string CommunicationTriggerId { get; set; }
    public CommunicationTrigger CommunicationTrigger { get; set; }

    [StringLength(32)]
    public string CommunicationTemplateId { get; set; }
    public CommunicationTemplate CommunicationTemplate { get; set; }
    #endregion

    #endregion

    #region "Private Methods"
    #endregion
}
