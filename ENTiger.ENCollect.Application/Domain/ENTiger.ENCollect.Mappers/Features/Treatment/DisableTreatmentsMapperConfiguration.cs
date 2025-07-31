using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DisableTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DisableTreatmentsMapperConfiguration() : base()
        {
            CreateMap<DisableTreatmentsDto, Treatment>();
        }
    }
}