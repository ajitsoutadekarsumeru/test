using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPerformanceBandMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnPerformanceBandMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnPerformanceBandDto, TreatmentOnPerformanceBand>();
            CreateMap<TreatmentOnPerformanceBand, TreatmentOnPerformanceBandDto>();
            CreateMap<TreatmentOnPerformanceBandDtoWithId, TreatmentOnPerformanceBand>();
            CreateMap<TreatmentOnPerformanceBand, TreatmentOnPerformanceBandDtoWithId>();
        }
    }
}