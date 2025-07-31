using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentHistoryDtoWithId : TreatmentHistoryDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}