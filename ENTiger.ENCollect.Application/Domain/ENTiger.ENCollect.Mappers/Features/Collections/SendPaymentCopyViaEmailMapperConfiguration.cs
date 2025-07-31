using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentCopyViaEmailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SendPaymentCopyViaEmailMapperConfiguration() : base()
        {
            CreateMap<SendPaymentCopyViaEmailDto, Collection>();
        }
    }
}