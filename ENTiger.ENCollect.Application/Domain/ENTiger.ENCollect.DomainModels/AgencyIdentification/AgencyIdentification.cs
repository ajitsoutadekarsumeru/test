using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentification : TFlexIdentification<AgencyIdentification, AgencyIdentificationDoc, Agency>
    {
        protected readonly ILogger<AgencyIdentification> _logger;

        public AgencyIdentification()
        {
        }

        public AgencyIdentification(ILogger<AgencyIdentification> logger)
        {
            _logger = logger;
        }
    }
}