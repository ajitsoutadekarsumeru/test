using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodeTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserVerificationCodeTypesMapperConfiguration() : base()
        {
            CreateMap<UserVerificationCodeTypesDto, UserVerificationCodeTypes>();
            CreateMap<UserVerificationCodeTypes, UserVerificationCodeTypesDto>();
            CreateMap<UserVerificationCodeTypesDtoWithId, UserVerificationCodeTypes>();
            CreateMap<UserVerificationCodeTypes, UserVerificationCodeTypesDtoWithId>();
        }
    }
}