using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentOnAccountDtoWithId : TreatmentOnAccountDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}