namespace ENTiger.ENCollect
{
    public partial class CompanyUserARMScopeOfWorkDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Bucket { get; set; }
        public string? City { get; set; }
        public string? ProductGroup { get; set; }
        public string? SubProduct { get; set; }
        public string? Product { get; set; }
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? State { get; set; }
        public string? Branch { get; set; }
        public string? ManagerId { get; set; }
        public string? ManagerFirstName { get; set; }
        public string? ManagerLastName { get; set; }
        public string? ReportingAgencyId { get; set; }
        public string? ReportingAgencyFirstName { get; set; }
        public string? ReportingAgencyLastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}