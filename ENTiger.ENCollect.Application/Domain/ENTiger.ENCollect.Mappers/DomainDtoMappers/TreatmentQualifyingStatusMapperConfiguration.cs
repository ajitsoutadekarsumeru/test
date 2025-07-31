using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentQualifyingStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentQualifyingStatusMapperConfiguration() : base()
        {
            CreateMap<TreatmentQualifyingStatusDto, TreatmentQualifyingStatus>();
            CreateMap<TreatmentQualifyingStatus, TreatmentQualifyingStatusDto>();
            CreateMap<TreatmentQualifyingStatusDtoWithId, TreatmentQualifyingStatus>();
            CreateMap<TreatmentQualifyingStatus, TreatmentQualifyingStatusDtoWithId>();
        }
    }
}