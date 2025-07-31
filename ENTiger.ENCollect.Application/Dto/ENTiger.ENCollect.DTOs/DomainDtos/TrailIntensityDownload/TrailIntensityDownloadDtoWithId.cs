using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TrailIntensityDownloadDtoWithId : TrailIntensityDownloadDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}