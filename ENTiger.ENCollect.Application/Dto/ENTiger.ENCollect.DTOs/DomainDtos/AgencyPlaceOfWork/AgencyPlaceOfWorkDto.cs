namespace ENTiger.ENCollect
{
    public partial class AgencyPlaceOfWorkDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? ProductGroup { get; set; }
        public string? SubProduct { get; set; }
        public string? Product { get; set; }
        public string? Bucket { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? State { get; set; }
        public string? Zone { get; set; }
        public string? ManagerId { get; set; }
    }
}