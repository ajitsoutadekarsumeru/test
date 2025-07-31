using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountLabels : DomainModelBridge
    {
        protected readonly ILogger<AccountLabels> _logger;

        protected AccountLabels()
        {
        }

        public AccountLabels(ILogger<AccountLabels> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public string Name { get; set; }

        public string Label { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}