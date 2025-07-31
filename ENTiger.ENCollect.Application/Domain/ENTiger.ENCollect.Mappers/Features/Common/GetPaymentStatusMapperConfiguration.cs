using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPaymentStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPaymentStatusMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetPaymentStatusDto>();
        }
    }
}