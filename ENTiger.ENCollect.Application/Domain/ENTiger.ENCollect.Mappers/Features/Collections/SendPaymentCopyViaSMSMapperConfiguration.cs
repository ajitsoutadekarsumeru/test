using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentCopyViaSMSMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SendPaymentCopyViaSMSMapperConfiguration() : base()
        {
            CreateMap<SendPaymentCopyViaSMSDto, Collection>();
        }
    }
}