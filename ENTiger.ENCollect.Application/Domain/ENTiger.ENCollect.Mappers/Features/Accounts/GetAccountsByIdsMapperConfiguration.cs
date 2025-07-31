using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAccountsByIdsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetAccountsByIdsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAccountsByIdsDto>();

        }
    }
}
