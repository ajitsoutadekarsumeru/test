using Sumeru.Flex;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepartmentsListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepartmentsListMapperConfiguration() : base()
        {
            CreateMap<Department, GetDepartmentsListDto>();
        }
    }
}