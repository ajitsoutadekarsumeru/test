using System.ComponentModel.DataAnnotations;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace ENTiger.ENCollect;

public partial class CommunicationTemplateDetail : DomainModelBridge
{
    protected readonly ILogger<CommunicationTemplateDetail> _logger;
    protected CommunicationTemplateDetail()
    {
    }
    public CommunicationTemplateDetail(ILogger<CommunicationTemplateDetail> logger)
    {
        _logger = logger;
    }

    #region "Attributes"

    #region "Public"

    [StringLength(50)]
    public string Language { get; set; }
    [StringLength(200)]
    public string? Subject { get; set; }
    [StringLength(5000)]
    public string Body { get; set; }
    public int Version { get; set; }

    [StringLength(32)]
    public string CommunicationTemplateId { get; set; }
    public CommunicationTemplate CommunicationTemplate { get; set; }

    #endregion "Public"

    #endregion "Attributes"
}