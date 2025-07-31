using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentOnPOSDtoWithId : TreatmentOnPOSDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}