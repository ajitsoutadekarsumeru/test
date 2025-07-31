using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionValidationMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DispositionValidationMasterMapperConfiguration() : base()
        {
            CreateMap<DispositionValidationMasterDto, DispositionValidationMaster>();
            CreateMap<DispositionValidationMaster, DispositionValidationMasterDto>();
            CreateMap<DispositionValidationMasterDtoWithId, DispositionValidationMaster>();
            CreateMap<DispositionValidationMaster, DispositionValidationMasterDtoWithId>();
        }
    }
}