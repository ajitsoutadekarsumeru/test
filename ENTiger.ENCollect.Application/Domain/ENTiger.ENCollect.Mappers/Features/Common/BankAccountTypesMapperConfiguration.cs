using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankAccountTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BankAccountTypesMapperConfiguration() : base()
        {
            CreateMap<BankAccountType, BankAccountTypesDto>();
        }
    }
}