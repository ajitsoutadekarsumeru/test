using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetACMDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetACMDetailsMapperConfiguration() : base()
        {
            CreateMap<UserAccessRights, GetACMDetailsDto>();
        }
    }
}