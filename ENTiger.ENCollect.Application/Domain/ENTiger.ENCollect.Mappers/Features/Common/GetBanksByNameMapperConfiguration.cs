using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBanksByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBanksByNameMapperConfiguration() : base()
        {
            CreateMap<Bank, GetBanksByNameDto>()
                .ForMember(o => o.BankName, opt => opt.MapFrom(o => o.Name));
        }
    }
}