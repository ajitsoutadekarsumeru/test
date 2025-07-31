using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class IdConfigMaster : DomainModelBridge
    {
        protected readonly ILogger<Sequence> _logger;

        public IdConfigMaster()
        {
        }

        public IdConfigMaster(ILogger<Sequence> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string CodeType { get; set; }

        public int BaseValue { get; set; }
        public int LatestValue { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}