using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

public partial class CommunicationTemplate : DomainModelBridge
{
    protected readonly ILogger<CommunicationTemplate> _logger;
    protected CommunicationTemplate()
    {
    }
    public CommunicationTemplate(ILogger<CommunicationTemplate> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"
    [StringLength(50)]
    public string Name { get; set; }
    public string TemplateType { get; set; }
    [StringLength(150)]
    public string EntryPoint { get; set; } // Account | User
    [StringLength(150)]
    public string RecipientType { get; set; } // Customer | Agent
    public int Version { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAvailableInAccountDetails { get; set; }
    public ICollection<CommunicationTemplateDetail> CommunicationTemplateDetails { get; set; } = new List<CommunicationTemplateDetail>();
    public ICollection<TriggerDeliverySpec> TemplateTriggers { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}