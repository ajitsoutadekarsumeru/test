using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankBranch : DomainModelBridge
    {
        protected readonly ILogger<BankBranch> _logger;

        protected BankBranch()
        {
        }

        public BankBranch(ILogger<BankBranch> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(50)]
        public string? MICR { get; set; }

        public Bank Bank { get; set; }

        [StringLength(32)]
        public string? BankId { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}