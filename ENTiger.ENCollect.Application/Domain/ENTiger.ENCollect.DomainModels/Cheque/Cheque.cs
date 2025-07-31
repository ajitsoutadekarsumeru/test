using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("Cheques")]
    public partial class Cheque : DomainModelBridge
    {
        protected readonly ILogger<Cheque> _logger;

        protected Cheque()
        {
        }

        public Cheque(ILogger<Cheque> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? BankName { get; set; }

        [StringLength(100)]
        public string? BranchName { get; set; }

        [StringLength(50)]
        public string? InstrumentNo { get; set; }

        public DateTime? InstrumentDate { get; set; }

        [StringLength(50)]
        public string? MICRCode { get; set; }

        [StringLength(50)]
        public string? IFSCCode { get; set; }

        [StringLength(50)]
        public string? BankCity { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}