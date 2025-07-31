namespace ENTiger.ENCollect
{
    public partial class CompanyUserScopeOfWorkDto : DtoBridge
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
        public string? DepartmentId { get; set; }
        public string? DesignationId { get; set; }
        public bool IsDeleted { get; set; }
    }
}