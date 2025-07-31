using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetKeyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetKeyMapperConfiguration() : base()
        {
            CreateMap<GetKeyDto, UserLoginKeys>();
        }
    }
}