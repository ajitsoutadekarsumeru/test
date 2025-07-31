namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByBaseBranchIdDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? StaffName { get; set; }
    }
}