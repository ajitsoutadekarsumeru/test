using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    [Table("AreaBaseBranchMappings")]
    public partial class AreaBaseBranchMapping : DomainModelBridge
    {
        protected readonly ILogger<Bank> _logger;

        protected AreaBaseBranchMapping()
        {
        }

        public AreaBaseBranchMapping(ILogger<Bank> logger)
        {
            _logger = logger;
        }

        [StringLength(32)]
        public string AreaId { get; set; }

        public Area Area { get; set; }

        [StringLength(32)]
        public string BaseBranchId { get; set; }

        public BaseBranch BaseBranch { get; set; }
    }
}