using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepartmentTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DepartmentTypeMapperConfiguration() : base()
        {
            CreateMap<DepartmentTypeDto, DepartmentType>();
            CreateMap<DepartmentType, DepartmentTypeDto>();
            CreateMap<DepartmentTypeDtoWithId, DepartmentType>();
            CreateMap<DepartmentType, DepartmentTypeDtoWithId>();
        }
    }
}