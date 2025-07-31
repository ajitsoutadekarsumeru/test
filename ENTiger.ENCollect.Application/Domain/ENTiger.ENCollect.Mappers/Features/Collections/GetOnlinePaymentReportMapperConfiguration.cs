using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetOnlinePaymentReportMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetOnlinePaymentReportMapperConfiguration() : base()
        {
            CreateMap<Collection, GetOnlinePaymentReportDto>();
        }
    }
}