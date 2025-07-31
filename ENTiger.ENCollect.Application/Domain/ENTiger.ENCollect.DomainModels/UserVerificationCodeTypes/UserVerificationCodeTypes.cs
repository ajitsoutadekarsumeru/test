using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodeTypes : DomainModelBridge
    {
        protected readonly ILogger<UserVerificationCodeTypes> _logger;

        protected UserVerificationCodeTypes()
        {
        }

        public UserVerificationCodeTypes(ILogger<UserVerificationCodeTypes> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(800)]
        public string? Description { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}