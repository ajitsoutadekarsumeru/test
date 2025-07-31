using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCustomerPersona : DomainModelBridge
    {
        protected readonly ILogger<UserCustomerPersona> _logger;

        protected UserCustomerPersona()
        {
        }

        public UserCustomerPersona(ILogger<UserCustomerPersona> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(32)]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}