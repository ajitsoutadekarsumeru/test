using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SubTreatmentDtoWithId : SubTreatmentDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}