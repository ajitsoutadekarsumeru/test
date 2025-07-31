using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RoundRobinTreatmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RoundRobinTreatmentMapperConfiguration() : base()
        {
            CreateMap<RoundRobinTreatmentDto, RoundRobinTreatment>();
            CreateMap<RoundRobinTreatment, RoundRobinTreatmentDto>();
            CreateMap<RoundRobinTreatmentDtoWithId, RoundRobinTreatment>();
            CreateMap<RoundRobinTreatment, RoundRobinTreatmentDtoWithId>();
        }
    }
}