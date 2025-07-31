using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistory : DomainModelBridge
    {
        protected readonly ILogger<TreatmentHistory> _logger;

        public TreatmentHistory()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentHistory>>();
        }

        public TreatmentHistory(ILogger<TreatmentHistory> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment? Treatment { get; set; }

        [StringLength(50)]
        public string? NoOfAccounts { get; set; }

        [StringLength(10)]
        public string? LatestStamping { get; set; }

        public int? SubTreatmentOrder { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}