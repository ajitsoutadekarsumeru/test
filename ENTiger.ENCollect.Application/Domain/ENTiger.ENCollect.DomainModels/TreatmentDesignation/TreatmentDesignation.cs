using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentDesignation : DomainModelBridge
    {
        protected readonly ILogger<TreatmentDesignation> _logger;

        public TreatmentDesignation()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentDesignation>>();
        }

        public TreatmentDesignation(ILogger<TreatmentDesignation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment? Treatment { get; set; }

        public Department? Department { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Designation? Designation { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}