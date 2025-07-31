namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class  GetUserTransactionLimitsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? TransactionType { get; set; }
        public int CurrentConsumption { get; set; }

        public int Limit { get; set; }

        public decimal PercentUsed { get; set; }
        public string? ColourCode { get; set; }
    }
}