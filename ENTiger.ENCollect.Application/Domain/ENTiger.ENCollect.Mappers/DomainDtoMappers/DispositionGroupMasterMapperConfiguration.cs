using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionGroupMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DispositionGroupMasterMapperConfiguration() : base()
        {
            CreateMap<DispositionGroupMasterDto, DispositionGroupMaster>();
            CreateMap<DispositionGroupMaster, DispositionGroupMasterDto>();
            CreateMap<DispositionGroupMasterDtoWithId, DispositionGroupMaster>();
            CreateMap<DispositionGroupMaster, DispositionGroupMasterDtoWithId>();
        }
    }
}