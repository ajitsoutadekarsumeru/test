using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoMaster : DomainModelBridge
    {
        protected readonly ILogger<GeoMaster> _logger;

        protected GeoMaster()
        {
        }

        public GeoMaster(ILogger<GeoMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"
        public string? Country { get; set; }

        //public string? Zone { get; set; }
        public string? Region { get; set; }

        public string? State { get; set; }
        public string? CITY { get; set; }
        public string? Area { get; set; }

        [StringLength(32)]
        public string? BaseBranchId { get; set; }

        public BaseBranch? BaseBranch { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}