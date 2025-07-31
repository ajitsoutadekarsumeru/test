using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentDesignationDtoWithId : TreatmentDesignationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}