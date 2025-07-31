namespace ENTiger.ENCollect
{
    public partial class CompanyUserDesignationDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CompanyUserId { get; set; }
        public string? DepartmentId { get; set; }
        public string? DesignationId { get; set; }
        public string? AccountabilityTypeId { get; set; }
    }
}