using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class MasterFileStatusDto : DtoBridge
    {
        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        public DateTime? FileProcessedDateTime { get; set; }

        public DateTime? FileUploadedDate { get; set; }

        [StringLength(300)]
        public string? Status { get; set; }

        [StringLength(100)]
        public string? UploadType { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(32)]
        public string? CreatedBy { get; protected set; }
    }
}