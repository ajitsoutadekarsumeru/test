using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentHistoryDetailsDtoWithId : TreatmentHistoryDetailsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}