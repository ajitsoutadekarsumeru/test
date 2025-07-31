using ENTiger.ENCollect.CommonModule;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class searchAgentDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? AgentName { get; set; }
        public string? AgentCode { get; set; }
        public string? AgencyName { get; set; }
        public string? UserType { get; set; }
        public DateTime? AuthorizationCardExpiry { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<UserDesignationOutputApiModel>? Roles { get; set; }
        public string? Status { get; set; }
        public decimal WalletLimit { get; set; }
    }
}