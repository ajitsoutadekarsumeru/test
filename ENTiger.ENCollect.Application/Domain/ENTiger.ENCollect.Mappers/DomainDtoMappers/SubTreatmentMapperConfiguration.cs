using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubTreatmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SubTreatmentMapperConfiguration() : base()
        {
            CreateMap<SubTreatmentDto, SubTreatment>();
            CreateMap<SubTreatment, SubTreatmentDto>();
            CreateMap<SubTreatmentDtoWithId, SubTreatment>();
            CreateMap<SubTreatment, SubTreatmentDtoWithId>();
        }
    }
}