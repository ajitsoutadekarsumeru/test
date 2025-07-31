using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentificationDoc : TFlexIdentificationDoc<AgencyUserIdentification, AgencyUserIdentificationDoc, AgencyUser>
    {
        protected readonly ILogger<AgencyUserIdentificationDoc> _logger;

        public AgencyUserIdentificationDoc()
        {
        }

        public AgencyUserIdentificationDoc(ILogger<AgencyUserIdentificationDoc> logger)
        {
            _logger = logger;
        }
    }
}