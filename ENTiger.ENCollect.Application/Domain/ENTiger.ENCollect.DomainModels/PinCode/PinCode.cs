using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("PinCodes")]
    public partial class PinCode : DomainModelBridge
    {
        protected readonly ILogger<AreaPinCodeMapping> _logger;

        protected PinCode()
        {
        }

        public PinCode(ILogger<AreaPinCodeMapping> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(500)]
        public string? Value { get; set; }

        public ICollection<AreaPinCodeMapping>? Areas { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}