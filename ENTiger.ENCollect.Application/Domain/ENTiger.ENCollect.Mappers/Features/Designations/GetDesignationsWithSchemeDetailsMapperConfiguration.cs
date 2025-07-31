using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetDesignationsWithSchemeDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetDesignationsWithSchemeDetailsMapperConfiguration() : base()
        {
            CreateMap<Designation, GetDesignationsWithSchemeDetailsDto>()
             .ForMember(o => o.DepartmentName, opt => opt.MapFrom(o => o.Department.Name))
             .ForMember(o => o.PermissionSchemeName, opt => opt.MapFrom(o => o.PermissionScheme.Name));
        }
    }
}
