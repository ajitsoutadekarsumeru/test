using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCustomerPersonaMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserCustomerPersonaMapperConfiguration() : base()
        {
            CreateMap<UserCustomerPersonaDto, UserCustomerPersona>();
            CreateMap<UserCustomerPersona, UserCustomerPersonaDto>();
            CreateMap<UserCustomerPersonaDtoWithId, UserCustomerPersona>();
            CreateMap<UserCustomerPersona, UserCustomerPersonaDtoWithId>();
        }
    }
}