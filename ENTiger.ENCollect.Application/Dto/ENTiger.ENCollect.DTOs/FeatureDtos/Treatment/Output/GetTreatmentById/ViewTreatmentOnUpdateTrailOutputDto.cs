namespace ENTiger.ENCollect
{
    public class ViewTreatmentOnUpdateTrailOutputDto
    {
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string DispositionCodeGroup { get; set; }

        public string DispositionCode { get; set; }

        public DateTime? NextActionDate { get; set; }

        public decimal? PTPAmount { get; set; }

        public bool? IsDeleted { get; set; }
    }
}