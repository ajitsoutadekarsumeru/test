using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgentDepartmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgentDepartmentsMapperConfiguration() : base()
        {
            CreateMap<Department, AgentDepartmentsDto>()
                .ForMember(o => o.DepartmentName, opt => opt.MapFrom(o => o.Name));
        }
    }
}