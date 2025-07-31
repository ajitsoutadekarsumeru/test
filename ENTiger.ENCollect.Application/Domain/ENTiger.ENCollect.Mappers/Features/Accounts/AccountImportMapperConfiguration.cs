using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountImportMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AccountImportMapperConfiguration() : base()
        {
            CreateMap<AccountImportDto, MasterFileStatus>();
        }
    }
}