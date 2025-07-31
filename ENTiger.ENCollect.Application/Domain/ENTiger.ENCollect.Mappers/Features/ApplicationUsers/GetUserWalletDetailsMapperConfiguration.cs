using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUserWalletDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetUserWalletDetailsMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, GetUserWalletDetailsDto>()
            .ForMember(o => o.WalletLimit, opt => opt.MapFrom(o => o.Wallet != null && o.Wallet.WalletLimit > 0 ? Math.Round(o.Wallet.WalletLimit,2) : 0))
            .ForMember(o => o.AvailableWallet, opt => opt.MapFrom(o => o.Wallet != null ? Math.Round(o.Wallet.EffectiveAvailableFunds,2) : 0))
            .ForMember(o => o.UsedWallet, opt => opt.MapFrom(o => o.Wallet != null ? Math.Round(o.Wallet.EffectiveUtilizedFunds,2) : 0))
            .ForMember(o => o.WalletUsedPercentage, opt => opt.MapFrom(o => o.Wallet != null ? Math.Round(o.Wallet.EffectiveUtilizedPercentage,2) : 0));


        }
    }
}
