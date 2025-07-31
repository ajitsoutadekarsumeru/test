using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTreatmentsMapperConfiguration() : base()
        {
            CreateMap<Treatment, GetTreatmentsDto>();
        }
    }
}