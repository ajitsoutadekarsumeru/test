using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountContactHistoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public AccountContactHistoryMapperConfiguration() : base()
        {
            CreateMap<AccountContactHistoryDto, AccountContactHistory>();
            CreateMap<AccountContactHistory, AccountContactHistoryDto>();
            CreateMap<AccountContactHistoryDtoWithId, AccountContactHistory>();
            CreateMap<AccountContactHistory, AccountContactHistoryDtoWithId>();

        }
    }
}
