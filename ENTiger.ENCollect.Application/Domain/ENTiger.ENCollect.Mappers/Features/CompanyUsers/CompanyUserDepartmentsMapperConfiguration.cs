using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDepartmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserDepartmentsMapperConfiguration() : base()
        {
            CreateMap<Department, CompanyUserDepartmentsDto>()
                .ForMember(o => o.DepartmentName, opt => opt.MapFrom(o => o.Name));
        }
    }
}