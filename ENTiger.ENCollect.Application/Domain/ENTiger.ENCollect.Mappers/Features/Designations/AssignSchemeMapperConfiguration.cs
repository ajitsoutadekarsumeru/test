using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AssignSchemeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public AssignSchemeMapperConfiguration() : base()
        {
            CreateMap<AssignSchemeDto, Designation>();

        }
    }
}
