using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountLabelsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAccountLabelsMapperConfiguration() : base()
        {
            CreateMap<AccountLabels, GetAccountLabelsDto>();
        }
    }
}