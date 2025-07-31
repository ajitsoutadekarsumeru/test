using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Bank : DomainModelBridge
    {
        protected readonly ILogger<Bank> _logger;

        public Bank()
        {
        }

        public Bank(ILogger<Bank> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        #endregion "Public"

        #region "Protected"
        [StringLength(200)]
        public string? Name { get; protected set; }

        //public ICollection<BankBranch> BankBranches { get; set; }
        #endregion "Protected"

        #region "Private"
        #endregion "Private"

        #endregion "Attributes"

        #region "Private Methods"
        public Bank disableBank(Bank bank, string userId)
        {
            bank.IsDeleted = true;
            bank.LastModifiedBy = userId;
            return bank;
        }

        #endregion "Private Methods"
    }
}