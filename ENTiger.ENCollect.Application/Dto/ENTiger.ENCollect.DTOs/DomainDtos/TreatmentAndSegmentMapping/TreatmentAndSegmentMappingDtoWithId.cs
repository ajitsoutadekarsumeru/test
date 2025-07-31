using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentAndSegmentMappingDtoWithId : TreatmentAndSegmentMappingDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}