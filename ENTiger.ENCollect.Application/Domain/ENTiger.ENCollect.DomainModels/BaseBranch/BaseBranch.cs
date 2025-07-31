using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranch : ApplicationOrg
    {
        protected readonly ILogger<BaseBranch> _logger;

        public BaseBranch()
        {
        }

        public BaseBranch(ILogger<BaseBranch> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? BankSolID { get; set; }

        [StringLength(50)]
        public string? NickName { get; set; }

        public string? AddressLine2 { get; set; }

        public string? AddressLine1 { get; set; }

        [StringLength(32)]
        public string? CityId { get; set; }

        public Cities? City { get; set; }

        public ICollection<AreaBaseBranchMapping> Areas { get; set; }

        [StringLength(50)]
        public string Zone { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Private Methods"

        public BaseBranch disableBaseBranch(BaseBranch baseBranch, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return baseBranch;
        }

        #endregion "Private Methods"
    }
}