using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Company : DomainModelBridge
    {
        protected readonly ILogger<Company> _logger;

        protected Company()
        {
        }

        public Company(ILogger<Company> logger)
        {
            _logger = logger;
        }
    }
}