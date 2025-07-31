using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class InsightSaveDownloadDetailsDto : DtoBridge
    {
        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

    }
}