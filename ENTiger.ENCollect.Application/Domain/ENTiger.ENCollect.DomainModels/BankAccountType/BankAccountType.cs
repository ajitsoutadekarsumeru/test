using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankAccountType : DomainModelBridge
    {
        protected readonly ILogger<BankAccountType> _logger;

        protected BankAccountType()
        {
        }

        public BankAccountType(ILogger<BankAccountType> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Value { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}