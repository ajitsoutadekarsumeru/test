using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTreatmentsSequenceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateTreatmentsSequenceMapperConfiguration() : base()
        {
            CreateMap<UpdateTreatmentsSequenceDto, Treatment>();
        }
    }
}