using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetMyReceiptAllocationsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetMyReceiptAllocationsMapperConfiguration() : base()
        {
            CreateMap<Receipt, GetMyReceiptAllocationsDto>()
                 .ForMember(o => o.ReceiptNo, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}