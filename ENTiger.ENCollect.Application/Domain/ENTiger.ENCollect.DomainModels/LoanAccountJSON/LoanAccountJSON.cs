using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountJSON : DomainModelBridge
    {
        protected readonly ILogger<LoanAccountJSON> _logger;

        public LoanAccountJSON()
        {
        }

        public LoanAccountJSON(ILogger<LoanAccountJSON> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(32)]
        public string? AccountId { get; set; }

        public LoanAccount Account { get; set; }

        public string? AccountJSON { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}