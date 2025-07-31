namespace ENTiger.ENCollect
{
    public class ViewTreatmentSegmentMappingTreatmentOutputDto
    {
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string SegmentId { get; set; }

        public string SegmentName { get; set; }

        public bool? IsDeleted { get; set; }
    }
}