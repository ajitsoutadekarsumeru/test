using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationsByDepartmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDesignationsByDepartmentMapperConfiguration() : base()
        {
            CreateMap<Designation, GetDesignationsByDepartmentDto>()
                .ForMember(d => d.DepartmentName, s => s.MapFrom(s => s.Department.Name))
                .ForMember(d => d.DesignationTypeName, s => s.MapFrom(s => s.DesignationType.Name));
        }
    }
}