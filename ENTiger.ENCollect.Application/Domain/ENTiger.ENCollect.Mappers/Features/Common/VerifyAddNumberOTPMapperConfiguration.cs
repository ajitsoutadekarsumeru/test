using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyAddNumberOTPMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public VerifyAddNumberOTPMapperConfiguration() : base()
        {
            CreateMap<VerifyAddNumberOTPDto, UserVerificationCodes>();
        }
    }
}