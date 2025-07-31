namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoggedInUserDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? AgencyCode { get; set; }
        public string? CustomId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set { }
        }

        public string? PrimaryMobileNumber { get; set; }
        public string? PrimaryEMail { get; set; }
        public string? SecondaryContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImage { get; set; }
        public string? Status { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? LastRenewalDate { get; set; }
        public string? AgencyId { get; set; }
        public string? AgencyFirstName { get; set; }
        public string? AgencyLastName { get; set; }
        public string? BaseBranchId { get; set; }
        public string? BaseBranchFirstName { get; set; }
        public string? BaseBranchLastName { get; set; }
        public string? City { get; set; }
        public string? DiallerName { get; set; }
        public string? EmployeeId { get; set; }
        public DateTime? LastlogoutTime { get; set; }
        public bool verified { get; set; }
        public string? AgencyAddress { get; set; }
        public string? AgencyPincode { get; set; }
        public string? AgencyCity { get; set; }
        public string? AuthorizationCardDateExpiryMessage { get; set; }
        public string? LastSuccessLoginMessage { get; set; }
        public string? LastFailLoginMessage { get; set; }
        public string? BloodGroup { get; set; }      
        public string? EmergencyContactNo { get; set; }
        public string? IdCardNumber { get; set; }

        public string? UserType { get; set; }
        public bool IsPolicyAccepted { get; set; } = false;
        public DateTime? PolicyAcceptedDate { get; set; }
        public ICollection<UserDesignationDto>? Roles { get; set; }
        public ICollection<ResponsibilityInfoDto>? ResponsibilityInfos { get; set; }

        public string? ProductLevelId { get; set; }
        public string? GeoLevelId { get; set; }
        public ICollection<UserProductScopeDto>? ProductScopes { get; set; }
        public ICollection<UserGeoScopeDto>? GeoScopes { get; set; }
        public ICollection<UserBucketScopeDto>? BucketScopes { get; set; }
    }
}