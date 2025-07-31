using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApplicationUserDetails : DomainModelBridge
    {
        protected readonly ILogger<ApplicationUserDetails> _logger;

        protected ApplicationUserDetails()
        {
        }

        public ApplicationUserDetails(ILogger<ApplicationUserDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(20)]
        public string? PanNumber { get; set; }

        [StringLength(20)]
        public string? AadharNumber { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        [StringLength(32)]
        public string? AddressId { get; set; }

        public Address address { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}