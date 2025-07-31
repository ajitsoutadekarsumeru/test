using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionCodeMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DispositionCodeMasterMapperConfiguration() : base()
        {
            CreateMap<DispositionCodeMasterDto, DispositionCodeMaster>();
            CreateMap<DispositionCodeMaster, DispositionCodeMasterDto>();
            CreateMap<DispositionCodeMasterDtoWithId, DispositionCodeMaster>();
            CreateMap<DispositionCodeMaster, DispositionCodeMasterDtoWithId>();
        }
    }
}