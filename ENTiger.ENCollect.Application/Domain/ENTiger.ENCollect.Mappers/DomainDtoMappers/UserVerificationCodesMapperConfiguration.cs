using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserVerificationCodesMapperConfiguration() : base()
        {
            CreateMap<UserVerificationCodesDto, UserVerificationCodes>();
            CreateMap<UserVerificationCodes, UserVerificationCodesDto>();
            CreateMap<UserVerificationCodesDtoWithId, UserVerificationCodes>();
            CreateMap<UserVerificationCodes, UserVerificationCodesDtoWithId>();
        }
    }
}