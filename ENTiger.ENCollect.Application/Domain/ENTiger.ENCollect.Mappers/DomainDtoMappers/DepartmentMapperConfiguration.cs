using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepartmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DepartmentMapperConfiguration() : base()
        {
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDtoWithId, Department>();
            CreateMap<Department, DepartmentDtoWithId>();
        }
    }
}