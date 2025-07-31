using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUserWalletDetailsDto : DtoBridge
    {
        public decimal? WalletLimit { get; set; }
        public decimal? AvailableWallet { get; set; }
        public decimal UsedWallet { get; set; }
        public decimal? WalletUsedPercentage { get; set; }
        public string? WalletUsedColor { get; set; }
    }
}
