using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentUpdateIntermediateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentUpdateIntermediateMapperConfiguration() : base()
        {
            CreateMap<TreatmentUpdateIntermediateDto, TreatmentUpdateIntermediate>();
            CreateMap<TreatmentUpdateIntermediate, TreatmentUpdateIntermediateDto>();
            CreateMap<TreatmentUpdateIntermediateDtoWithId, TreatmentUpdateIntermediate>();
            CreateMap<TreatmentUpdateIntermediate, TreatmentUpdateIntermediateDtoWithId>();
        }
    }
}