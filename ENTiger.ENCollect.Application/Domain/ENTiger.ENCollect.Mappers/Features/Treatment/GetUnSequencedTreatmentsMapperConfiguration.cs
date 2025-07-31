using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUnSequencedTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUnSequencedTreatmentsMapperConfiguration() : base()
        {
            CreateMap<Treatment, GetUnSequencedTreatmentsDto>();
        }
    }
}