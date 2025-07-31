using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountLabelsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AccountLabelsMapperConfiguration() : base()
        {
            CreateMap<AccountLabelsDto, AccountLabels>();
            CreateMap<AccountLabels, AccountLabelsDto>();
            CreateMap<AccountLabelsDtoWithId, AccountLabels>();
            CreateMap<AccountLabels, AccountLabelsDtoWithId>();
        }
    }
}