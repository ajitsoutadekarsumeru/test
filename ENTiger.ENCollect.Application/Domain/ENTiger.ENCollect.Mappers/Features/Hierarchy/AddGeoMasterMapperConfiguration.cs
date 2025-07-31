using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddGeoMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public AddGeoMasterMapperConfiguration() : base()
        {
            CreateMap<AddGeoMasterDto, HierarchyMaster>();

        }
    }
}
