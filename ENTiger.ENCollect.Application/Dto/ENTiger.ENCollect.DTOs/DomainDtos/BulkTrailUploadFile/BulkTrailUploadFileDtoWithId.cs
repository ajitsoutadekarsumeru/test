using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class BulkTrailUploadFileDtoWithId : BulkTrailUploadFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}