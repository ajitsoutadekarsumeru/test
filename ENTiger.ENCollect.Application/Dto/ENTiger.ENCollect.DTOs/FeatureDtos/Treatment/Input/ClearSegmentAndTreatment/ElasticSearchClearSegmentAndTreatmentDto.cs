namespace ENTiger.ENCollect
{
    public class ElasticSearchClearSegmentAndTreatmentDto : DtoBridge
    {
        public string id { get; set; }
        public string segmentationid { get; set; }
        public string treatmentid { get; set; }

        public string agreementid { get; set; }
    }
}