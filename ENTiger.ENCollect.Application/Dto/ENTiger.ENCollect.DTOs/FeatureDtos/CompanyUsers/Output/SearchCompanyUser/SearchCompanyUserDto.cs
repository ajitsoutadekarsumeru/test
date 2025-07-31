namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCompanyUserDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? PrimaryMobileNumber { get; set; }
        public string? EmployeeId { get; set; }
        public string? CustomId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public string? SinglePointReportingManagerId { get; set; }
        public string? SinglePointReportingManagerFirstName { get; set; }
        public string? SinglePointReportingManagerLastName { get; set; }
        public bool? IsDeactivated { get; set; }
        public string? BaseBranchId { get; set; }
        public string? BaseBranchFirstName { get; set; }
        public string? BaseBranchLastName { get; set; }
        public string? status { get; set; }
        public string? ENCollectId { get; set; }
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public string? WorkOfficeLongitude { get; set; }
        public string? WorkOfficeLattitude { get; set; }
        public string? PrimaryContactAreaCode { get; set; }
        public decimal WalletLimit { get; set; }
        public string? UserType { get; set; }
    }
}