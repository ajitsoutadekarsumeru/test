namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByAccountabilityDto : DtoBridge
    {
        public string? userId { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Mobile { get; set; }
        public ICollection<ResponsibilityInfo>? ResponsibilityInfos { get; set; }
        public ICollection<UserDesignationOutputApiModel>? Roles { get; set; }
    }

    public class ResponsibilityInfo
    {
        public string? Id { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
    }

    public class UserDesignationOutputApiModel
    {
        public string? Id { get; set; }
        public string? CompanyUserId { get; set; }
        public string? DepartmentId { get; set; }
        public string? DesignationId { get; set; }
        public string? AccountabilityTypeId { get; set; }
        public string DesignationName { get; set; }
        public bool IsPrimaryDesignation { get; set; }
    }
}