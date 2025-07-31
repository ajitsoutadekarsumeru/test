using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendOTPToVerifyNumberMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SendOTPToVerifyNumberMapperConfiguration() : base()
        {
            CreateMap<SendOTPToVerifyNumberDto, UserVerificationCodes>();
        }
    }
}