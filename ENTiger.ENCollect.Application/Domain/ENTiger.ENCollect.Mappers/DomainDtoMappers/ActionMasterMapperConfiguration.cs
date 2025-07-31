using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ActionMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ActionMasterMapperConfiguration() : base()
        {
            CreateMap<ActionMasterDto, ActionMaster>();
            CreateMap<ActionMaster, ActionMasterDto>();
            CreateMap<ActionMasterDtoWithId, ActionMaster>();
            CreateMap<ActionMaster, ActionMasterDtoWithId>();
        }
    }
}