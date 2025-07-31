using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentQualifyingStatusDtoWithId : TreatmentQualifyingStatusDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}