using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BankListMapperConfiguration() : base()
        {
            CreateMap<Bank, BankListDto>()
                .ForMember(o => o.BankName, opt => opt.MapFrom(o => o.Name));
        }
    }
}