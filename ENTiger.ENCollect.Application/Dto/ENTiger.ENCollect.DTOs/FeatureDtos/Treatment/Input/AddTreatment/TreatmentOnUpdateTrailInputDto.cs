namespace ENTiger.ENCollect
{
    public class TreatmentOnUpdateTrailInputDto
    {
        public string? Id { get; set; }

        public string? DispositionCodeGroup { get; set; }

        public string? DispositionCode { get; set; }

        public DateTime? NextActionDate { get; set; }

        public decimal? PTPAmount { get; set; }
    }
}