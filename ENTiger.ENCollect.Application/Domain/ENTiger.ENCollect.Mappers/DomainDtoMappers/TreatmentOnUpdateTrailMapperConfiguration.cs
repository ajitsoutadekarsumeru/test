using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnUpdateTrailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnUpdateTrailMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnUpdateTrailDto, TreatmentOnUpdateTrail>();
            CreateMap<TreatmentOnUpdateTrail, TreatmentOnUpdateTrailDto>();
            CreateMap<TreatmentOnUpdateTrailDtoWithId, TreatmentOnUpdateTrail>();
            CreateMap<TreatmentOnUpdateTrail, TreatmentOnUpdateTrailDtoWithId>();
        }
    }
}