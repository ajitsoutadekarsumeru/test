using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class LoginDetailsHistory : DomainModelBridge
    {
        protected readonly ILogger<MenuMaster> _logger;

        public LoginDetailsHistory()
        {
        }

        public LoginDetailsHistory(ILogger<MenuMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(32)]
        public string? UserId { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? LoginStatus { get; set; }

        [StringLength(2000)]
        public string? LoginInputJson { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}