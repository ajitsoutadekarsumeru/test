using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentAndSegmentMappingMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentAndSegmentMappingMapperConfiguration() : base()
        {
            CreateMap<TreatmentAndSegmentMappingDto, TreatmentAndSegmentMapping>();
            CreateMap<TreatmentAndSegmentMapping, TreatmentAndSegmentMappingDto>();
            CreateMap<TreatmentAndSegmentMappingDtoWithId, TreatmentAndSegmentMapping>();
            CreateMap<TreatmentAndSegmentMapping, TreatmentAndSegmentMappingDtoWithId>();
        }
    }
}