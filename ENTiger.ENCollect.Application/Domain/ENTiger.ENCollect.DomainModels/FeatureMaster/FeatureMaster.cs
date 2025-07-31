using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class FeatureMaster : DomainModelBridge
    {
        protected readonly ILogger<FeatureMaster> _logger;

        protected FeatureMaster()
        {
        }

        public FeatureMaster(ILogger<FeatureMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"
        public string Parameter { get; protected set; }
        public string Value { get; protected set; }
        public bool IsEnabled { get; protected set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}