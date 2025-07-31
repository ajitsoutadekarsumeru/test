using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnAccount : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnAccount> _logger;

        public TreatmentOnAccount()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnAccount>>();
        }

        public TreatmentOnAccount(ILogger<TreatmentOnAccount> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment? Treatment { get; set; }

        [StringLength(32)]
        public string? SubTreatmentId { get; set; }

        public SubTreatment? SubTreatment { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Department? Department { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        public Designation? designation { get; set; }

        [StringLength(50)]
        public string? Percentage { get; set; }

        [StringLength(50)]
        public string? AllocationId { get; set; }

        [StringLength(100)]
        public string? AllocationName { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}