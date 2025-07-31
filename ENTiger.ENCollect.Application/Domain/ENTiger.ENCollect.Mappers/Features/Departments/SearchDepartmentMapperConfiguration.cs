using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchDepartmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchDepartmentMapperConfiguration() : base()
        {
            CreateMap<Department, SearchDepartmentDto>();
        }
    }
}