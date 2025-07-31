using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCurrentLocationDetails : DomainModelBridge
    {
        protected readonly ILogger<UserCurrentLocationDetails> _logger;

        protected UserCurrentLocationDetails()
        {
        }

        public UserCurrentLocationDetails(ILogger<UserCurrentLocationDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}