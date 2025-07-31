using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentLinkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SendPaymentLinkMapperConfiguration() : base()
        {
            CreateMap<SendPaymentLinkDto, Collection>();
        }
    }
}