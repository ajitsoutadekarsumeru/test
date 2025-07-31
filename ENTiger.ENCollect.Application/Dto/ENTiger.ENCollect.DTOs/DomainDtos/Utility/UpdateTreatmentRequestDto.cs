using System.Data;

namespace ENTiger.ENCollect
{
    public partial class UpdateTreatmentRequestDto : DtoBridge
    {
       
        public DataTable Data { get; set; }
        public string WorkRequestId { get; set; }

        // Only needed for MSSQL
        public string StoredProcedure { get; set; }
        public string TenantId { get; set; }
    }
}