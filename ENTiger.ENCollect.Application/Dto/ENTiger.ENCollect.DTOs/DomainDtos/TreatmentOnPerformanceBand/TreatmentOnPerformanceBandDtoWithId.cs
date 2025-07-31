using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentOnPerformanceBandDtoWithId : TreatmentOnPerformanceBandDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}