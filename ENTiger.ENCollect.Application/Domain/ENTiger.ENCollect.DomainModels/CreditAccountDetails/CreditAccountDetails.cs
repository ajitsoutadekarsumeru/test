using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreditAccountDetails : DomainModelBridge
    {
        protected readonly ILogger<CreditAccountDetails> _logger;

        protected CreditAccountDetails()
        {
        }

        public CreditAccountDetails(ILogger<CreditAccountDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(200)]
        public string? AccountHolderName { get; set; }

        [StringLength(50)]
        public string? BankAccountNo { get; set; }

        public BankBranch BankBranch { get; set; }

        [StringLength(32)]
        public string? BankBranchId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}