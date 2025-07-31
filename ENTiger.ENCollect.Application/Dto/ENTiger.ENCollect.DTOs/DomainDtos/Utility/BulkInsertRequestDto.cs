using System.Data;

namespace ENTiger.ENCollect
{
    public partial class BulkInsertRequestDto : DtoBridge
    {
        public string TenantId { get; set; }
        public string TableName { get; set; }
        public DataTable Table { get; set; }
        public string FileName { get; set; }
        public string FieldTerminator { get; set; } = ","; // Default: CSV format
        public string LineTerminator { get; set; } = "\n"; // Default: Unix-style
        public int Timeout { get; set; } = 600;
        public int NumberOfLinesToSkip { get; set; } = 1;  // Default: Skip header row
    }
}