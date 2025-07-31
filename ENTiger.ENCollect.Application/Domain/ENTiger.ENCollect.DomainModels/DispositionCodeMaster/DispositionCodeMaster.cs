using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionCodeMaster : DomainModelBridge
    {
        protected readonly ILogger<DispositionCodeMaster> _logger;

        public DispositionCodeMaster()
        {
        }

        public DispositionCodeMaster(ILogger<DispositionCodeMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public DispositionGroupMaster DispositionGroupMaster { get; set; }

        public string? DispositionGroupMasterId { get; set; }
        public Int64 SrNo { get; set; }

        public string? DispositionCode { get; set; }
        public string? Permissibleforfieldagent { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }

        [StringLength(150)]
        public string? DispositionAccess { get; set; }

        //TODO: review DispositionCodeMaster class - change in property to bool
        //[StringLength(50)]
        //public string? DispositionCodeCustomerOrAccountLevel { get; set; }
        public bool DispositionCodeIsCustomerLevel { get; set; } = false;

        #endregion "Public"

        #endregion "Attributes"

        #region "Private Methods"

        public DispositionCodeMaster disableDispositionCodeMaster(DispositionCodeMaster dispositionCodeMaster, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return dispositionCodeMaster;
        }

        #endregion "Private Methods"
    }
}