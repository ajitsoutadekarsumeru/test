using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class ApplicationOrg : OrgBridge
    {
        [StringLength(100)]
        public string? CustomId { get; protected set; }
        [StringLength(20)]
        public string? TransactionSource { get; set; }
    }
}