using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CollectionUploadFileDto : DtoBridge
    {
        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(250)]
        public string? FileName { get; set; }

        [StringLength(250)]
        public string? FilePath { get; set; }

        public DateTime FileUploadedDate { get; set; }

        public DateTime FileProcessedDateTime { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }
       

        [StringLength(32)]
        public string? CreatedBy { get; protected set; }
    }

}
