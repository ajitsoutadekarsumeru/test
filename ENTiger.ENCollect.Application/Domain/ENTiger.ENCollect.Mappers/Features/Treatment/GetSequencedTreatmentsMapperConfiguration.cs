using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSequencedTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSequencedTreatmentsMapperConfiguration() : base()
        {
            CreateMap<Treatment, GetSequencedTreatmentsDto>();
        }
    }
}