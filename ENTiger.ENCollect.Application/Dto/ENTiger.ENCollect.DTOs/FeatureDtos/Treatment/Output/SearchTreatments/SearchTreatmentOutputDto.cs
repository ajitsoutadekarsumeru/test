namespace ENTiger.ENCollect
{
    public class SearchTreatmentOutputDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Mode { get; set; }

        public string CreatedBy { get; set; }
        public bool? IsDisabled { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public string ExecutionHistory { get; set; }
    }
}