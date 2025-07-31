using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentificationDoc : TFlexIdentificationDoc<AgencyIdentification, AgencyIdentificationDoc, Agency>
    {
        protected readonly ILogger<AgencyIdentificationDoc> _logger;

        public AgencyIdentificationDoc()
        {
        }

        public AgencyIdentificationDoc(ILogger<AgencyIdentificationDoc> logger)
        {
            _logger = logger;
        }
    }
}