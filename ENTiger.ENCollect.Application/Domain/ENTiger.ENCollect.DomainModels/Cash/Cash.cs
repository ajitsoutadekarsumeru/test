using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Cash : DomainModelBridge
    {
        protected readonly ILogger<Cash> _logger;

        protected Cash()
        {
        }

        public Cash(ILogger<Cash> logger)
        {
            _logger = logger;
        }
    }
}