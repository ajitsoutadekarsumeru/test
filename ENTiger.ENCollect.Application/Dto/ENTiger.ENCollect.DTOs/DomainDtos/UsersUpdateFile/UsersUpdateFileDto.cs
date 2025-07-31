using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UsersUpdateFileDto : DtoBridge
    {
        public string CustomId { get; set; }

        [StringLength(100)]
        public string UploadType { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        [StringLength(250)]
        public string FilePath { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime UploadedDate { get; set; }

        public DateTime? ProcessedDateTime { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string CreatedBy { get; set; }
    }
}