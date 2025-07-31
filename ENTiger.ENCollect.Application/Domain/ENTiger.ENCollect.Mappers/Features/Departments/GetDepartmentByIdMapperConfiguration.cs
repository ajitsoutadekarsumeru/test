using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepartmentByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepartmentByIdMapperConfiguration() : base()
        {
            CreateMap<Department, GetDepartmentByIdDto>();
        }
    }
}