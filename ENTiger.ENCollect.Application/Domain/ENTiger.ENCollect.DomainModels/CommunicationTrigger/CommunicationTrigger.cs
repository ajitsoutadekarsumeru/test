using Elastic.Clients.Elasticsearch.IndexManagement;
using ENTiger.ENCollect.DomainModels;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class CommunicationTrigger : DomainModelBridge
{
    protected readonly ILogger<CommunicationTrigger> _logger;
    public CommunicationTrigger()
    {
        _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<CommunicationTrigger>>();
    }
    public CommunicationTrigger(string name,
        int daysOffset,
        string description, string UserId, string recipientType)
    {
        Id = SequentialGuid.NewGuidString();
        Name = name;
        DaysOffset = daysOffset;
        IsActive = true;
        Description = description;
        CreatedBy = UserId;
        RecipientType= recipientType;
        TrackingState = TrackingState.Added;
    }

    #region "Attributes"
    [StringLength(50)]
    public string Name { get; set; }
    public int DaysOffset { get; set; } // Used for XDays* condition types
    public bool IsActive { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    [StringLength(150)]
    public string RecipientType { get; set; } // Customer | Agent
    public string TriggerTypeId { get; set; }
    public TriggerType TriggerType { get; set; }

    // Association to multiple templates
    private readonly List<TriggerDeliverySpec> _triggerTemplates = new();
    public List<TriggerDeliverySpec> TriggerTemplates => _triggerTemplates;
   

    #region "Public"
    // Associate a Template aggregate with this Trigger
    public void AssociateTemplate(string templateId, string userId)
    {
        //add the template to the trigger
        if (string.IsNullOrEmpty(templateId))
            throw new ArgumentNullException(nameof(templateId), "Template ID cannot be null or empty.");
        
        var template = new TriggerDeliverySpec(this.Id, templateId, userId);
        _triggerTemplates.Add(template);
    }

    // Remove a Template association
    public void DissociateTemplate(string templateId)
    {
        //Gaurd clause to ensure templateId is not null or empty
        if (string.IsNullOrEmpty(templateId))
            throw new ArgumentNullException(nameof(templateId), "Template ID cannot be null or empty.");

        var templateTriggerMap = _triggerTemplates.FirstOrDefault(r => r.Id == templateId)
               ?? throw new InvalidOperationException("Template not found.");
        templateTriggerMap.SetIsDeleted(true);
        templateTriggerMap.SetDeleted(); // ✅
    }
    public void Enable() => IsActive = true;
    public void Disable() => IsActive = false;
    #endregion

    #endregion

    #region "Private Methods"
    #endregion
}
