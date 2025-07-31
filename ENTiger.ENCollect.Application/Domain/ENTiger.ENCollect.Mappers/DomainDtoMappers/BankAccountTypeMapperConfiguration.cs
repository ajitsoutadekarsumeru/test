using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankAccountTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BankAccountTypeMapperConfiguration() : base()
        {
            CreateMap<BankAccountTypeDto, BankAccountType>();
            CreateMap<BankAccountType, BankAccountTypeDto>();
            CreateMap<BankAccountTypeDtoWithId, BankAccountType>();
            CreateMap<BankAccountType, BankAccountTypeDtoWithId>();
        }
    }
}