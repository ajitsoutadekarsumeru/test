using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentification : TFlexIdentification<AgencyUserIdentification, AgencyUserIdentificationDoc, AgencyUser>
    {
        protected readonly ILogger<AgencyUserIdentification> _logger;

        public AgencyUserIdentification()
        {
        }

        public AgencyUserIdentification(ILogger<AgencyUserIdentification> logger)
        {
            _logger = logger;
        }
    }
}