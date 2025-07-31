namespace ENTiger.ENCollect
{
    public class ElasticSearchSimulateLoanAccountDto : DtoBridge
    {
        public string id { get; set; }
        public double? bom_pos { get; set; }
        public string segmentationid { get; set; }

        public string agreementid { get; set; }
        public string paymentstatus { get; set; }
        public string createddate { get; set; }

        public string modeofoperation { get; set; }

        public int counter { get; set; }
    }
}