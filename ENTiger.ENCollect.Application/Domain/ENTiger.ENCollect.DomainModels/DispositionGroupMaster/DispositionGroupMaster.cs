using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionGroupMaster : DomainModelBridge
    {
        protected readonly ILogger<DispositionGroupMaster> _logger;

        protected DispositionGroupMaster()
        {
        }

        public DispositionGroupMaster(ILogger<DispositionGroupMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public Int64 SrNo { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }

        [StringLength(150)]
        public string DispositionAccess { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}