using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentUpdateIntermediateDtoWithId : TreatmentUpdateIntermediateDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}