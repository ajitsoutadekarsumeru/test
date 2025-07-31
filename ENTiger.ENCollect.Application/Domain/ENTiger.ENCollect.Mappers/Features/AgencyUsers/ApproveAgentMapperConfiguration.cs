using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApproveAgentMapperConfiguration() : base()
        {
            CreateMap<ApproveAgentDto, AgencyUser>()
                .ForMember(d => d.Remarks, s => s.MapFrom(s => s.Description));
        }
    }
}