using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentMapperConfiguration() : base()
        {
            CreateMap<TreatmentDto, Treatment>();
            CreateMap<Treatment, TreatmentDto>();
            CreateMap<TreatmentDtoWithId, Treatment>();
            CreateMap<Treatment, TreatmentDtoWithId>();
        }
    }
}