using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepositBankMaster : DomainModelBridge
    {
        protected readonly ILogger<DepositBankMaster> _logger;

        public DepositBankMaster()
        {
        }

        public DepositBankMaster(ILogger<DepositBankMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? DepositBankName { get; set; }

        [StringLength(100)]
        public string? DepositBranchName { get; set; }

        [StringLength(50)]
        public string? DepositAccountNumber { get; set; }

        [StringLength(100)]
        public string? AccountHolderName { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Private Methods"

        public DepositBankMaster disableDepositBankMaster(DepositBankMaster depositBankMaster, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return depositBankMaster;
        }

        #endregion "Private Methods"
    }
}