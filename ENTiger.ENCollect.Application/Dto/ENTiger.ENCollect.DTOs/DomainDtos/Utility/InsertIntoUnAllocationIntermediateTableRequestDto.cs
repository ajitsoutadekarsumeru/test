using System.Data;

namespace ENTiger.ENCollect
{
    public partial class InsertIntoUnAllocationIntermediateTableRequestDto : DtoBridge
    {
        public string? TableName { get; set; }
        public DataTable Table { get; set; }
        public string? TenantId { get; set; }
        public string? UserId { get; set; }       
        public string? WorkRequestId { get; set; }
             
    }
}