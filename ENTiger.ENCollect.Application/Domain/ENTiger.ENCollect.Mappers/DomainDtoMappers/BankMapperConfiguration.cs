using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BankMapperConfiguration() : base()
        {
            CreateMap<BankDto, Bank>();
            CreateMap<Bank, BankDto>();
            CreateMap<BankDtoWithId, Bank>();
            CreateMap<Bank, BankDtoWithId>();
        }
    }
}