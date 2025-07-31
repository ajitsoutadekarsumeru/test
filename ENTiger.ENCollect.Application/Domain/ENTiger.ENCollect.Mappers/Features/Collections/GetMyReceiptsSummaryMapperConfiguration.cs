using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyReceiptsSummaryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMyReceiptsSummaryMapperConfiguration() : base()
        {
            CreateMap<Collection, GetMyReceiptsSummaryDto>();

        }
    }
}
