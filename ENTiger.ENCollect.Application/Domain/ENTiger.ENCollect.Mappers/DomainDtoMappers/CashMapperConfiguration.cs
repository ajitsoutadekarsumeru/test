using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CashMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CashMapperConfiguration() : base()
        {
            CreateMap<CashDto, Cash>();
            CreateMap<Cash, CashDto>();
            CreateMap<CashDtoWithId, Cash>();
            CreateMap<Cash, CashDtoWithId>();
        }
    }
}