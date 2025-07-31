using System.Data;

namespace ENTiger.ENCollect
{
    public partial class InsertIntermediateTableRequestDto : DtoBridge
    {
        public string? TableName { get; set; }
        public DataTable Table { get; set; }
        public string? FileName { get; set; }
        public string? Delimiter { get; set; } = "|";
      
        public string TenantId { get; set; }
        public Dictionary<string, string> ExtraTableColumns { get; set; } = null;
    }
}