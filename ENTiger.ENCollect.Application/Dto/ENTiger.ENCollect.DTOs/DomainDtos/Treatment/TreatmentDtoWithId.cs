using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentDtoWithId : TreatmentDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}