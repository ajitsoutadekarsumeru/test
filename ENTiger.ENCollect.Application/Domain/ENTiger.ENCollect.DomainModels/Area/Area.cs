using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    [Table("Areas")]
    public partial class Area : DomainModelBridge
    {
        protected readonly ILogger<Bank> _logger;

        protected Area()
        {
        }

        public Area(ILogger<Bank> logger)
        {
            _logger = logger;
        }

        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        [StringLength(32)]
        public string CityId { get; set; }

        public Cities City { get; set; }

        [StringLength(32)]
        public string BaseBranchId { get; set; }

        public BaseBranch BaseBranch { get; set; }
    }
}