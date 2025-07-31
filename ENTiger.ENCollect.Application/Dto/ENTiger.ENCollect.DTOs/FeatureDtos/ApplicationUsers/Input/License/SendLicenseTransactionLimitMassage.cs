namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class SendLicenseTransactionLimitMassageDto : DtoBridge
    {
        public string UserName { get; set; }
        public string TransactionType { get; set; }
        public int Limit { get; set; }
        public List<GetUserTransactionLimitsDto> TransactionTypeDetails { get; set; }
    }
}
