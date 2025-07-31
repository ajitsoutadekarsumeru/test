using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TrailGapDownloadDtoWithId : TrailGapDownloadDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}