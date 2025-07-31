using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepartmentTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepartmentTypesMapperConfiguration() : base()
        {
            CreateMap<DepartmentType, GetDepartmentTypesDto>();
        }
    }
}