namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountContactDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public String? AGREEMENTID { get; set; }
        public String? LatestMobileNo { get; set; }
        public String? MAILINGMOBILE { get; set; }
        public String? MAILINGPHONE1 { get; set; }
        public String? MAILINGPHONE2 { get; set; }
        public String? NONMAILINGMOBILE { get; set; }
        public String? NONMAILINGMOBILE1 { get; set; }
        public String? NONMAILINGMOBILE2 { get; set; }
        public String? NONMAILINGPHONE1 { get; set; }
        public String? NONMAILINGPHONE2 { get; set; }
        public String? REF1_CONTACT { get; set; }
        public String? REF2_CONTACT { get; set; }
    }
}