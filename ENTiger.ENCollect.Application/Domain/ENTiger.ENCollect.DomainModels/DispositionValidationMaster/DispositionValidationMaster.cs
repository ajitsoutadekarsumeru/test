using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionValidationMaster : DomainModelBridge
    {
        protected readonly ILogger<DispositionValidationMaster> _logger;

        protected DispositionValidationMaster()
        {
        }

        public DispositionValidationMaster(ILogger<DispositionValidationMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public DispositionCodeMaster DispositionCodeMaster { get; set; }
        public string DispositionCodeMasterId { get; set; }
        public string validationFieldName { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}