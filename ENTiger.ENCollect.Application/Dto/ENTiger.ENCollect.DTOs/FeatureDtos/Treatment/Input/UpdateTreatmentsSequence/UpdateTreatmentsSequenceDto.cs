using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class UpdateTreatmentsSequenceDto : DtoBridge
    {
        public List<TreatmentSequenceDto> input { get; set; }
    }

    public class TreatmentSequenceDto : DtoBridge
    {
        [Required]
        public string Id { get; set; }

        public int? Sequence { get; set; }
    }
}