using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("AreaPinCodeMappings")]
    public partial class AreaPinCodeMapping : DomainModelBridge
    {
        protected readonly ILogger<AreaPinCodeMapping> _logger;

        protected AreaPinCodeMapping()
        {
        }

        public AreaPinCodeMapping(ILogger<AreaPinCodeMapping> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(500)]
        public Area? Area { get; set; }

        [StringLength(32)]
        public string? AreaId { get; set; }

        public PinCode? PinCode { get; set; }

        [StringLength(32)]
        public string? PinCodeId { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}